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
    public class TransacoesController : ControllerBase
    {
        private readonly IServiceTransacao _servicoTransacao;
    
    public TransacoesController(IServiceTransacao servicoTransacao)
    {
        _servicoTransacao = servicoTransacao;
    }
    
    /// <summary>
    /// Obter todas as transações cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoRespostaDTO>>> ObterTodos()
    {
        var transacoes = await _servicoTransacao.ObterTodosAsync();
        return Ok(transacoes);
    }
    
    /// <summary>
    /// Obter uma transação pelo ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoRespostaDTO>> ObterPorId(Guid id)
    {
        try
        {
            var transacao = await _servicoTransacao.ObterPorIdAsync(id);
            return Ok(transacao);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Obter transações por pessoa
    /// </summary>
    [HttpGet("pessoa/{pessoaId}")]
    public async Task<ActionResult<IEnumerable<TransacaoRespostaDTO>>> ObterPorPessoa(Guid pessoaId)
    {
        var transacoes = await _servicoTransacao.ObterPorPessoaAsync(pessoaId);
        return Ok(transacoes);
    }
    
    /// <summary>
    /// Obter transações por categoria
    /// </summary>
    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult<IEnumerable<TransacaoRespostaDTO>>> ObterPorCategoria(Guid categoriaId)
    {
        var transacoes = await _servicoTransacao.ObterPorCategoriaAsync(categoriaId);
        return Ok(transacoes);
    }
    
    /// <summary>
    /// Criar uma nova transação
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TransacaoRespostaDTO>> Criar([FromBody] CriarTransacaoDTO dto)
    {
        try
        {
            var transacao = await _servicoTransacao.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = transacao.Id }, transacao);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
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