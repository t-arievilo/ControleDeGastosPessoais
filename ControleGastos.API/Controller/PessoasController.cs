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
    public class PessoasController : ControllerBase
    {
        private readonly IServicePessoa _servicoPessoa;
    
    public PessoasController(IServicePessoa servicoPessoa)
    {
        _servicoPessoa = servicoPessoa;
    }
    
    /// <summary>
    /// Obter todas as pessoas cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaRespostaDTO>>> ObterTodos()
    {
        var pessoas = await _servicoPessoa.ObterTodosAsync();
        return Ok(pessoas);
    }
    
    /// <summary>
    /// Obter uma pessoa pelo ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PessoaRespostaDTO>> ObterPorId(Guid id)
    {
        try
        {
            var pessoa = await _servicoPessoa.ObterPorIdAsync(id);
            return Ok(pessoa);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Criar uma nova pessoa
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PessoaRespostaDTO>> Criar([FromBody] CriarPessoaDTO dto)
    {
        try
        {
            var pessoa = await _servicoPessoa.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Atualizar uma pessoa existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PessoaRespostaDTO>> Atualizar(Guid id, [FromBody] AtualizarPessoaDTO dto)
    {
        try
        {
            var pessoa = await _servicoPessoa.AtualizarAsync(id, dto);
            return Ok(pessoa);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Remover uma pessoa
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var removido = await _servicoPessoa.RemoverAsync(id);
        if (!removido)
            return NotFound(new { message = $"Pessoa com ID {id} não encontrada" });
        
        return NoContent();
    }
    }
}