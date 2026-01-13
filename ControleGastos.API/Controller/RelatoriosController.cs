using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Application.DTOs;
using ControleGastos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControleGastos.API.Controller
{
    [Route("[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IServiceRelatorio _servicoRelatorio;
    
    public RelatoriosController(IServiceRelatorio servicoRelatorio)
    {
        _servicoRelatorio = servicoRelatorio;
    }
    
    /// <summary>
    /// Obter relatório de totais por pessoa
    /// </summary>
    [HttpGet("totais-por-pessoa")]
    public async Task<ActionResult<RelatorioGeralDTO>> ObterTotaisPorPessoa()
    {
        var relatorio = await _servicoRelatorio.ObterTotaisPorPessoaAsync();
        return Ok(relatorio);
    }
    
    /// <summary>
    /// Obter relatório de totais por categoria
    /// </summary>
    [HttpGet("totais-por-categoria")]
    public async Task<ActionResult<IEnumerable<TotalPorCategoriaDTO>>> ObterTotaisPorCategoria()
    {
        var relatorio = await _servicoRelatorio.ObterTotaisPorCategoriaAsync();
        return Ok(relatorio);
    }
    }
}