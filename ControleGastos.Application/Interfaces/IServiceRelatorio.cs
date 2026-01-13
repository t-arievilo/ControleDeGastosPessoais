using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Application.DTOs;

namespace ControleGastos.Application.Interfaces
{
    public interface IServiceRelatorio
    {
    Task<RelatorioGeralDTO> ObterTotaisPorPessoaAsync();
    Task<IEnumerable<TotalPorCategoriaDTO>> ObterTotaisPorCategoriaAsync();  
    }
}