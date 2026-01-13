using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleGastos.Application.DTOs;
using ControleGastos.Domain.Entidades;

namespace ControleGastos.Application.Config
{
    public class MapperProfile: Profile 
    {
        public MapperProfile()
    {
        CreateMap<Pessoa, PessoaRespostaDTO>();
        CreateMap<Categoria, CategoriaRespostaDTO>();
        CreateMap<Transacao, TransacaoRespostaDTO>()
            .ForMember(dest => dest.CategoriaDescricao, opt => opt.MapFrom(src => src.Categoria.Descricao))
            .ForMember(dest => dest.PessoaNome, opt => opt.MapFrom(src => src.Pessoa.Nome))
            .ForMember(dest => dest.TipoDescricao, opt => opt.MapFrom(src => src.Tipo.ToString()));
    }
    }
}