using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Entidades;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.ViewModels;
using ControleGastos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infra.Repositorios
{
    public class RepositorioTransacao : RepositorioBase<Transacao>, IRepositorioTransacao
{
    public RepositorioTransacao(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Transacao>> ObterPorPessoaAsync(Guid pessoaId)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.Pessoa)
            .Where(t => t.PessoaId == pessoaId)
            .OrderByDescending(t => t.DataCriacao)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Transacao>> ObterPorCategoriaAsync(Guid categoriaId)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.Pessoa)
            .Where(t => t.CategoriaId == categoriaId)
            .OrderByDescending(t => t.DataCriacao)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Transacao>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim)
    {
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.Pessoa)
            .Where(t => t.DataCriacao >= inicio && t.DataCriacao <= fim)
            .OrderByDescending(t => t.DataCriacao)
            .ToListAsync();
    }
    
    public async Task<decimal> ObterTotalPorTipoEPessoaAsync(Guid pessoaId, TipoTransacao tipo)
    {
        return await _context.Transacoes
            .Where(t => t.PessoaId == pessoaId && t.Tipo == tipo)
            .SumAsync(t => t.Valor);
    }
    
    public async Task<IEnumerable<TotalPorPessoaViewModel>> ObterTotaisPorPessoaAsync()
    {
        return await _context.Pessoas
            .Select(p => new TotalPorPessoaViewModel
            {
                PessoaId = p.Id,
                Nome = p.Nome,
                Idade = p.Idade,
                TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            })
            .Select(x => new TotalPorPessoaViewModel
            {
                PessoaId = x.PessoaId,
                Nome = x.Nome,
                Idade = x.Idade,
                TotalReceitas = x.TotalReceitas,
                TotalDespesas = x.TotalDespesas,
                Saldo = x.TotalReceitas - x.TotalDespesas
            })
            .ToListAsync();
    }
    
    public async Task<IEnumerable<TotalPorCategoriaViewModel>> ObterTotaisPorCategoriaAsync()
    {
        return await _context.Categorias
            .Select(c => new TotalPorCategoriaViewModel
            {
                CategoriaId = c.Id,
                Descricao = c.Descricao,
                Finalidade = c.Finalidade,
                TotalReceitas = c.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                TotalDespesas = c.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            })
            .Select(x => new TotalPorCategoriaViewModel
            {
                CategoriaId = x.CategoriaId,
                Descricao = x.Descricao,
                Finalidade = x.Finalidade,
                TotalReceitas = x.TotalReceitas,
                TotalDespesas = x.TotalDespesas,
                Saldo = x.TotalReceitas - x.TotalDespesas
            })
            .ToListAsync();
    }
    }
}