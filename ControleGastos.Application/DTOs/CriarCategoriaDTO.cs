using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.DTOs
{
    public class CriarCategoriaDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoria Finalidade { get; set; }
    }

    public class CategoriaRespostaDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoria Finalidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public string FinalidadeDescricao { get; set; } = string.Empty;
    }
}