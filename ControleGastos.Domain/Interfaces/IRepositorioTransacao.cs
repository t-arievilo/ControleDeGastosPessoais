using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Entidades;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.ViewModels;

namespace ControleGastos.Domain.Interfaces
{
    public interface IRepositorioTransacao : IRepositorioBase<Transacao>
{
    Task<IEnumerable<Transacao>> ObterPorPessoaAsync(Guid pessoaId);
    Task<IEnumerable<Transacao>> ObterPorCategoriaAsync(Guid categoriaId);
    Task<IEnumerable<Transacao>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim);
    Task<decimal> ObterTotalPorTipoEPessoaAsync(Guid pessoaId, TipoTransacao tipo);
    Task<IEnumerable<TotalPorPessoaViewModel>> ObterTotaisPorPessoaAsync();
    Task<IEnumerable<TotalPorCategoriaViewModel>> ObterTotaisPorCategoriaAsync();
}
}