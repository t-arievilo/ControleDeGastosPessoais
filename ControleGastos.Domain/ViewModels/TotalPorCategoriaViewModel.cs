using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.ViewModels
{
    public class TotalPorCategoriaViewModel
    {
    public Guid CategoriaId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
    }
}