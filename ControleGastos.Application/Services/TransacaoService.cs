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
    public class TransacaoService : ServiceBase, IServiceTransacao
    {
         private readonly IRepositorioTransacao _repositorioTransacao;
    private readonly IRepositorioPessoa _repositorioPessoa;
    private readonly IRepositorioCategoria _repositorioCategoria;
    private readonly IMapper _mapper;
    
    public TransacaoService(
        IUnitOfWork unitOfWork,
        IRepositorioTransacao repositorioTransacao,
        IRepositorioPessoa repositorioPessoa,
        IRepositorioCategoria repositorioCategoria,
        IMapper mapper) : base(unitOfWork)
    {
        _repositorioTransacao = repositorioTransacao;
        _repositorioPessoa = repositorioPessoa;
        _repositorioCategoria = repositorioCategoria;
        _mapper = mapper;
    }
    
    public async Task<TransacaoRespostaDTO> CriarAsync(CriarTransacaoDTO dto)
    {
        // Validar pessoa
        var pessoa = await _repositorioPessoa.ObterPorIdAsync(dto.PessoaId);
        if (pessoa == null)
            throw new KeyNotFoundException($"Pessoa com ID {dto.PessoaId} não encontrada");
        
        // Validar categoria
        var categoria = await _repositorioCategoria.ObterPorIdAsync(dto.CategoriaId);
        if (categoria == null)
            throw new KeyNotFoundException($"Categoria com ID {dto.CategoriaId} não encontrada");
        
        // Validar se categoria pode ser usada para o tipo de transação
        if (!categoria.PodeSerUsadaPara(dto.Tipo))
            throw new InvalidOperationException(
                $"Categoria '{categoria.Descricao}' não pode ser usada para transações do tipo {dto.Tipo}");
        
        // Criar transação
        var transacao = new Transacao(
            dto.Descricao, 
            dto.Valor, 
            dto.Tipo, 
            dto.CategoriaId, 
            dto.PessoaId, 
            pessoa);
        
        await _repositorioTransacao.AdicionarAsync(transacao);
        await CommitAsync();
        
        return MapToRespostaDTO(transacao, pessoa, categoria);
    }
    
    public async Task<TransacaoRespostaDTO> ObterPorIdAsync(Guid id)
    {
        var transacao = await _repositorioTransacao.ObterPorIdAsync(id);
        if (transacao == null)
            throw new KeyNotFoundException($"Transação com ID {id} não encontrada");
        
        // Carregar relacionamentos
        var pessoa = await _repositorioPessoa.ObterPorIdAsync(transacao.PessoaId);
        var categoria = await _repositorioCategoria.ObterPorIdAsync(transacao.CategoriaId);
        
        return MapToRespostaDTO(transacao, pessoa!, categoria!);
    }
    
    public async Task<IEnumerable<TransacaoRespostaDTO>> ObterTodosAsync()
    {
        var transacoes = await _repositorioTransacao.ObterTodosAsync();
        var resultado = new List<TransacaoRespostaDTO>();
        
        foreach (var transacao in transacoes)
        {
            var pessoa = await _repositorioPessoa.ObterPorIdAsync(transacao.PessoaId);
            var categoria = await _repositorioCategoria.ObterPorIdAsync(transacao.CategoriaId);
            
            resultado.Add(MapToRespostaDTO(transacao, pessoa!, categoria!));
        }
        
        return resultado;
    }
    
    public async Task<IEnumerable<TransacaoRespostaDTO>> ObterPorPessoaAsync(Guid pessoaId)
    {
        var transacoes = await _repositorioTransacao.ObterPorPessoaAsync(pessoaId);
        var pessoa = await _repositorioPessoa.ObterPorIdAsync(pessoaId);
        
        return transacoes.Select(t => MapToRespostaDTO(t, pessoa!, t.Categoria));
    }
    
    public async Task<IEnumerable<TransacaoRespostaDTO>> ObterPorCategoriaAsync(Guid categoriaId)
    {
        var transacoes = await _repositorioTransacao.ObterPorCategoriaAsync(categoriaId);
        var categoria = await _repositorioCategoria.ObterPorIdAsync(categoriaId);
        
        return transacoes.Select(t => MapToRespostaDTO(t, t.Pessoa, categoria!));
    }
    
    private TransacaoRespostaDTO MapToRespostaDTO(
        Transacao transacao, 
        Pessoa pessoa, 
        Categoria categoria)
    {
        return new TransacaoRespostaDTO
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            DataCriacao = transacao.DataCriacao,
            CategoriaId = transacao.CategoriaId,
            CategoriaDescricao = categoria?.Descricao ?? "Desconhecida",
            PessoaId = transacao.PessoaId,
            PessoaNome = pessoa?.Nome ?? "Desconhecida",
            TipoDescricao = transacao.Tipo.ToString()
        };
    }
    }
}