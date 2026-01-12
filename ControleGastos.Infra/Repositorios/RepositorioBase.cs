using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Repositorios
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    protected RepositorioBase(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<T?> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public virtual async Task<IEnumerable<T>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public virtual async Task AdicionarAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task AtualizarAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task RemoverAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task<bool> ExisteAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
    }
}