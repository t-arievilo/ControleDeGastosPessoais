using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleGastos.Application.DTOs;
using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Entidades;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    public class PessoaService : ServiceBase, IServicePessoa
    {
       private readonly IRepositorioPessoa _repositorioPessoa;
    private readonly IMapper _mapper;
    
    public PessoaService(
        IUnitOfWork unitOfWork,
        IRepositorioPessoa repositorioPessoa,
        IMapper mapper) : base(unitOfWork)
    {
        _repositorioPessoa = repositorioPessoa;
        _mapper = mapper;
    }
    
    public async Task<PessoaRespostaDTO> CriarAsync(CriarPessoaDTO dto)
    {
        var pessoa = new Pessoa(dto.Nome, dto.Idade);
        await _repositorioPessoa.AdicionarAsync(pessoa);
        await CommitAsync();
        
        return MapToRespostaDTO(pessoa);
    }
    
    public async Task<PessoaRespostaDTO> ObterPorIdAsync(Guid id)
    {
        var pessoa = await _repositorioPessoa.ObterPorIdComTransacoesAsync(id);
        if (pessoa == null)
            throw new KeyNotFoundException($"Pessoa com ID {id} não encontrada");
        
        return MapToRespostaDTO(pessoa);
    }
    
    public async Task<IEnumerable<PessoaRespostaDTO>> ObterTodosAsync()
    {
        var pessoas = await _repositorioPessoa.ObterComTransacoesAsync();
        return pessoas.Select(MapToRespostaDTO);
    }
    
    public async Task<PessoaRespostaDTO> AtualizarAsync(Guid id, AtualizarPessoaDTO dto)
    {
        var pessoa = await _repositorioPessoa.ObterPorIdAsync(id);
        if (pessoa == null)
            throw new KeyNotFoundException($"Pessoa com ID {id} não encontrada");
        
        pessoa.Atualizar(dto.Nome, dto.Idade);
        await _repositorioPessoa.AtualizarAsync(pessoa);
        await CommitAsync();
        
        return MapToRespostaDTO(pessoa);
    }
    
    public async Task<bool> RemoverAsync(Guid id)
    {
        var pessoa = await _repositorioPessoa.ObterPorIdAsync(id);
        if (pessoa == null)
            return false;
        
        await _repositorioPessoa.RemoverAsync(pessoa);
        await CommitAsync();
        
        return true;
    }
    
    private PessoaRespostaDTO MapToRespostaDTO(Pessoa pessoa)
    {
        return new PessoaRespostaDTO
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade,
            DataCriacao = pessoa.DataCriacao,
            EhMenorDeIdade = pessoa.EhMenorDeIdade(),
            TotalTransacoes = pessoa.Transacoes?.Count ?? 0
        };
    } 
    }
}