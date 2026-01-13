using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Application.DTOs;

namespace ControleGastos.Application.Interfaces
{
    public interface IServiceCategoria
    {
    Task<CategoriaRespostaDTO> CriarAsync(CriarCategoriaDTO dto);
    Task<CategoriaRespostaDTO> ObterPorIdAsync(Guid id);
    Task<IEnumerable<CategoriaRespostaDTO>> ObterTodosAsync();
    Task<IEnumerable<CategoriaRespostaDTO>> ObterPorFinalidadeAsync(int finalidade);   
    }
}