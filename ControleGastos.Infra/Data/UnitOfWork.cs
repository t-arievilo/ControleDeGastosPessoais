using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infra.Repositorios;

namespace ControleGastos.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
         private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Pessoas = new RepositorioPessoa(context);
        Categorias = new RepositorioCategoria(context);
        Transacoes = new RepositorioTransacao(context);
    }
    
    public IRepositorioPessoa Pessoas { get; }
    public IRepositorioCategoria Categorias { get; }
    public IRepositorioTransacao Transacoes { get; }
    
    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task RollbackAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
    }
}