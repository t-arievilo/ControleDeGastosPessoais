using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Application.DTOs
{
    public class TotalPorPessoaDTO
    {
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
    }

    public class TotalPorCategoriaDTO
{
    public Guid CategoriaId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Finalidade { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}
    public class RelatorioGeralDTO
{
    public List<TotalPorPessoaDTO> TotaisPorPessoa { get; set; } = new();
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoGeral { get; set; }
}
}