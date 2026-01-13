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
    public class CategoriasController : ControllerBase
    {
       private readonly IServiceCategoria _servicoCategoria;
    
    public CategoriasController(IServiceCategoria servicoCategoria)
    {
        _servicoCategoria = servicoCategoria;
    }
    
    /// <summary>
    /// Obter todas as categorias cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaRespostaDTO>>> ObterTodos()
    {
        var categorias = await _servicoCategoria.ObterTodosAsync();
        return Ok(categorias);
    }
    
    /// <summary>
    /// Obter categorias por finalidade (0=Despesa, 1=Receita, 2=Ambas)
    /// </summary>
    [HttpGet("finalidade/{finalidade}")]
    public async Task<ActionResult<IEnumerable<CategoriaRespostaDTO>>> ObterPorFinalidade(int finalidade)
    {
        var categorias = await _servicoCategoria.ObterPorFinalidadeAsync(finalidade);
        return Ok(categorias);
    }
    
    /// <summary>
    /// Obter uma categoria pelo ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaRespostaDTO>> ObterPorId(Guid id)
    {
        try
        {
            var categoria = await _servicoCategoria.ObterPorIdAsync(id);
            return Ok(categoria);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Criar uma nova categoria
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CategoriaRespostaDTO>> Criar([FromBody] CriarCategoriaDTO dto)
    {
        try
        {
            var categoria = await _servicoCategoria.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = categoria.Id }, categoria);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    }
}