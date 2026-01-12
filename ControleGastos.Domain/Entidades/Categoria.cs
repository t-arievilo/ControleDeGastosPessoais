using System;
using System.Collections.Generic;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Entidades
{
    public class Categoria
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public FinalidadeCategoria Finalidade { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public virtual ICollection<Transacao> Transacoes { get; private set; }

        protected Categoria() { }

        // Construtor para uso do sistema (Gera ID automático)
        public Categoria(string descricao, FinalidadeCategoria finalidade)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            Finalidade = finalidade;
            DataCriacao = DateTime.UtcNow;
            Transacoes = new List<Transacao>();
            Validar();
        }

        // NOVO: Construtor para o SEED (Permite passar o ID manual)
        public Categoria(Guid id, string descricao, FinalidadeCategoria finalidade)
        {
            Id = id;
            Descricao = descricao;
            Finalidade = finalidade;
            DataCriacao = DateTime.UtcNow;
            Transacoes = new List<Transacao>();
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new ArgumentException("Descrição é obrigatória");
        }

        public bool PodeSerUsadaPara(TipoTransacao tipo)
        {
            return Finalidade switch
            {
                FinalidadeCategoria.Ambas => true,
                FinalidadeCategoria.Despesa => tipo == TipoTransacao.Despesa,
                FinalidadeCategoria.Receita => tipo == TipoTransacao.Receita,
                _ => false
            };
        }
    }
}