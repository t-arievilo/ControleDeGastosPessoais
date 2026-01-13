using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Application.DTOs;
using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    public class ServicoRelatorio : ServiceBase, IServiceRelatorio
{
    private readonly IRepositorioTransacao _repositorioTransacao;
    
    public ServicoRelatorio(
        IUnitOfWork unitOfWork,
        IRepositorioTransacao repositorioTransacao) : base(unitOfWork)
    {
        _repositorioTransacao = repositorioTransacao;
    }
    
    public async Task<RelatorioGeralDTO> ObterTotaisPorPessoaAsync()
    {
        var totais = await _repositorioTransacao.ObterTotaisPorPessoaAsync();
        
        var resultado = new RelatorioGeralDTO
        {
            TotaisPorPessoa = totais.Select(t => new TotalPorPessoaDTO
            {
                PessoaId = t.PessoaId,
                Nome = t.Nome,
                Idade = t.Idade,
                TotalReceitas = t.TotalReceitas,
                TotalDespesas = t.TotalDespesas,
                Saldo = t.Saldo
            }).ToList()
        };
        
        // Calcular totais gerais
        resultado.TotalGeralReceitas = resultado.TotaisPorPessoa.Sum(p => p.TotalReceitas);
        resultado.TotalGeralDespesas = resultado.TotaisPorPessoa.Sum(p => p.TotalDespesas);
        resultado.SaldoGeral = resultado.TotalGeralReceitas - resultado.TotalGeralDespesas;
        
        return resultado;
    }
    
    public async Task<IEnumerable<TotalPorCategoriaDTO>> ObterTotaisPorCategoriaAsync()
    {
        var totais = await _repositorioTransacao.ObterTotaisPorCategoriaAsync();
        
        return totais.Select(t => new TotalPorCategoriaDTO
        {
            CategoriaId = t.CategoriaId,
            Descricao = t.Descricao,
            Finalidade = t.Finalidade.ToString(),
            TotalReceitas = t.TotalReceitas,
            TotalDespesas = t.TotalDespesas,
            Saldo = t.Saldo
        }).ToList();
    }
    }
}