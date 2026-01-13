using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.DTOs
{
    public class CriarTransacaoDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid PessoaId { get; set; }
    }

    public class TransacaoRespostaDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid CategoriaId { get; set; }
        public string CategoriaDescricao { get; set; } = string.Empty;
        public Guid PessoaId { get; set; }
        public string PessoaNome { get; set; } = string.Empty;
        public string TipoDescricao { get; set; } = string.Empty;
    }
}