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
    public class CategoriaService : ServiceBase, IServiceCategoria
    {
        private readonly IRepositorioCategoria _repositorioCategoria;
    private readonly IMapper _mapper;
    
    public CategoriaService(
        IUnitOfWork unitOfWork,
        IRepositorioCategoria repositorioCategoria,
        IMapper mapper) : base(unitOfWork)
    {
        _repositorioCategoria = repositorioCategoria;
        _mapper = mapper;
    }
    
    public async Task<CategoriaRespostaDTO> CriarAsync(CriarCategoriaDTO dto)
    {
        // Verificar se já existe categoria com mesma descrição
        var existe = await _repositorioCategoria.ExistePorDescricaoAsync(dto.Descricao);
        if (existe)
            throw new InvalidOperationException($"Já existe uma categoria com a descrição '{dto.Descricao}'");
        
        var categoria = new Categoria(dto.Descricao, dto.Finalidade);
        await _repositorioCategoria.AdicionarAsync(categoria);
        await CommitAsync();
        
        return MapToRespostaDTO(categoria);
    }
    
    public async Task<CategoriaRespostaDTO> ObterPorIdAsync(Guid id)
    {
        var categoria = await _repositorioCategoria.ObterPorIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException($"Categoria com ID {id} não encontrada");
        
        return MapToRespostaDTO(categoria);
    }
    
    public async Task<IEnumerable<CategoriaRespostaDTO>> ObterTodosAsync()
    {
        var categorias = await _repositorioCategoria.ObterTodosAsync();
        return categorias.Select(MapToRespostaDTO);
    }
    
    public async Task<IEnumerable<CategoriaRespostaDTO>> ObterPorFinalidadeAsync(int finalidade)
    {
        var categorias = await _repositorioCategoria.ObterPorFinalidadeAsync(finalidade);
        return categorias.Select(MapToRespostaDTO);
    }
    
    private CategoriaRespostaDTO MapToRespostaDTO(Categoria categoria)
    {
        return new CategoriaRespostaDTO
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade,
            DataCriacao = categoria.DataCriacao,
            FinalidadeDescricao = categoria.Finalidade.ToString()
        };
    }
    }
}