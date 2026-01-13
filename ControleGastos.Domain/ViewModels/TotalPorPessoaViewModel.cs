using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Domain.ViewModels
{
    public class TotalPorPessoaViewModel
    {
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
    }
}