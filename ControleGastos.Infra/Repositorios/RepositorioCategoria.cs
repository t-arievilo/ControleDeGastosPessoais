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
    public class RepositorioCategoria : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoria(AppDbContext context) : base(context)
    {
    }
    
    public async Task<bool> ExistePorDescricaoAsync(string descricao)
    {
        return await _context.Categorias
            .AnyAsync(c => c.Descricao.ToLower() == descricao.ToLower());
    }
    
    public async Task<IEnumerable<Categoria>> ObterPorFinalidadeAsync(int finalidade)
    {
        return await _context.Categorias
            .Where(c => (int)c.Finalidade == finalidade || c.Finalidade == Domain.Enums.FinalidadeCategoria.Ambas)
            .ToListAsync();
    }
    }
}