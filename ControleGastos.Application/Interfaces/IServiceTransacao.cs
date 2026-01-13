using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Application.DTOs;

namespace ControleGastos.Application.Interfaces
{
    public interface IServiceTransacao
    {
    Task<TransacaoRespostaDTO> CriarAsync(CriarTransacaoDTO dto);
    Task<TransacaoRespostaDTO> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TransacaoRespostaDTO>> ObterTodosAsync();
    Task<IEnumerable<TransacaoRespostaDTO>> ObterPorPessoaAsync(Guid pessoaId);
    Task<IEnumerable<TransacaoRespostaDTO>> ObterPorCategoriaAsync(Guid categoriaId); 
    }
}