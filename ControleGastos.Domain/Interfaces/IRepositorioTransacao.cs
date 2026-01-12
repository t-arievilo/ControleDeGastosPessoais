using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Entidades;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Interfaces
{
    public interface IRepositorioTransacao : IRepositorioBase<Transacao>
    {
    Task<IEnumerable<Transacao>> ObterPorPessoaAsync(Guid pessoaId);
    Task<IEnumerable<Transacao>> ObterPorCategoriaAsync(Guid categoriaId);
    Task<IEnumerable<Transacao>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim);
    Task<decimal> ObterTotalPorTipoEPessoaAsync(Guid pessoaId, TipoTransacao tipo);
    Task<IEnumerable<object>> ObterTotaisPorPessoaAsync();
    Task<IEnumerable<object>> ObterTotaisPorCategoriaAsync();
    }
}