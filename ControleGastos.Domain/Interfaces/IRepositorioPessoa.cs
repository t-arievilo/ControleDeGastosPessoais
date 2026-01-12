using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Entidades;

namespace ControleGastos.Domain.Interfaces
{
    public interface IRepositorioPessoa : IRepositorioBase<Pessoa>
    {
    Task<IEnumerable<Pessoa>> ObterComTransacoesAsync();
    Task<Pessoa?> ObterPorIdComTransacoesAsync(Guid id);
    }
}