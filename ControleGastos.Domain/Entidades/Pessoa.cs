using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Domain.Entidades
{
    public class Pessoa
    {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public virtual ICollection<Transacao> Transacoes { get; private set; }

    protected Pessoa() { }

    public Pessoa(string nome, int idade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Idade = idade;
        DataCriacao = DateTime.UtcNow;
        Transacoes = new List<Transacao>();
        
        Validar();
    }

    private void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("Nome é obrigatório");
            
        if (Idade <= 0)
            throw new ArgumentException("Idade deve ser maior que zero");
            
        if (Idade > 150)
            throw new ArgumentException("Idade inválida");
    }

     public bool EhMenorDeIdade() => Idade < 18;
    
    public void Atualizar(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
        Validar();
    }
    
    }

}