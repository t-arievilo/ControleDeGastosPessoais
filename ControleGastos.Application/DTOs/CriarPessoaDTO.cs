using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Application.DTOs
{
    public class CriarPessoaDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }

    public class AtualizarPessoaDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }

    public class PessoaRespostaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool EhMenorDeIdade { get; set; }
        public int TotalTransacoes { get; set; }
    }
}