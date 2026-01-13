using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
    IRepositorioPessoa Pessoas { get; }
    IRepositorioCategoria Categorias { get; }
    IRepositorioTransacao Transacoes { get; }
    
    Task<int> CommitAsync();
    Task RollbackAsync();
    }
}