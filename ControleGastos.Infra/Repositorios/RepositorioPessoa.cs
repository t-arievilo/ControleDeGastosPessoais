using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Entidades;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Repositorios
{
    public class RepositorioPessoa : RepositorioBase<Pessoa>, IRepositorioPessoa
    {
         public RepositorioPessoa(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Pessoa>> ObterComTransacoesAsync()
    {
        return await _context.Pessoas
            .Include(p => p.Transacoes)
            .ThenInclude(t => t.Categoria)
            .ToListAsync();
    }
    
    public async Task<Pessoa?> ObterPorIdComTransacoesAsync(Guid id)
    {
        return await _context.Pessoas
            .Include(p => p.Transacoes)
            .ThenInclude(t => t.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    }
}