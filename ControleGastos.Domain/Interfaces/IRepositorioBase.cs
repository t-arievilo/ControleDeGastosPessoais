using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Domain.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
    Task<T?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<T>> ObterTodosAsync();
    Task AdicionarAsync(T entity);
    Task AtualizarAsync(T entity);
    Task RemoverAsync(T entity);
    Task<bool> ExisteAsync(Guid id);
} 
    }
