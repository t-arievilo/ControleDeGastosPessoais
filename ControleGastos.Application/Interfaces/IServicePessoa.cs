using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Application.DTOs;

namespace ControleGastos.Application.Interfaces
{
    public interface IServicePessoa
    {
    Task<PessoaRespostaDTO> CriarAsync(CriarPessoaDTO dto);
    Task<PessoaRespostaDTO> ObterPorIdAsync(Guid id);
    Task<IEnumerable<PessoaRespostaDTO>> ObterTodosAsync();
    Task<PessoaRespostaDTO> AtualizarAsync(Guid id, AtualizarPessoaDTO dto);
    Task<bool> RemoverAsync(Guid id);
    }
}