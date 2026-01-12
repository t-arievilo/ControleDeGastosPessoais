using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Entidades
{
    public class Transacao
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public DateTime DataCriacao { get; private set; }
    
    // Chaves estrangeiras
    public Guid CategoriaId { get; private set; }
    public Guid PessoaId { get; private set; }
    
    // Propriedades de navegação
    public virtual Categoria Categoria { get; private set; }
    public virtual Pessoa Pessoa { get; private set; }
    
    protected Transacao() { }
    
    public Transacao(string descricao, decimal valor, TipoTransacao tipo, 
                    Guid categoriaId, Guid pessoaId, Pessoa pessoa)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        CategoriaId = categoriaId;
        PessoaId = pessoaId;
        Pessoa = pessoa;
        DataCriacao = DateTime.UtcNow;
        
        Validar();
    }
    
    private void Validar()
    {
        if (string.IsNullOrWhiteSpace(Descricao))
            throw new ArgumentException("Descrição é obrigatória");
            
        if (Valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero");
            
        if (!Enum.IsDefined(typeof(TipoTransacao), Tipo))
            throw new ArgumentException("Tipo de transação inválido");
            
        if (CategoriaId == Guid.Empty)
            throw new ArgumentException("Categoria é obrigatória");
            
        if (PessoaId == Guid.Empty)
            throw new ArgumentException("Pessoa é obrigatória");
            
        // Regra: Menores de idade só podem ter despesas
        if (Pessoa != null && Pessoa.EhMenorDeIdade() && Tipo == TipoTransacao.Receita)
            throw new InvalidOperationException("Menores de idade só podem ter despesas");
    }
}
}