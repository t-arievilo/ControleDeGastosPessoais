using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Entidades;

namespace ControleGastos.Domain.Interfaces
{
    public interface IRepositorioCategoria : IRepositorioBase<Categoria>
    {
    Task<bool> ExistePorDescricaoAsync(string descricao);
    Task<IEnumerable<Categoria>> ObterPorFinalidadeAsync(int finalidade);
    }
}