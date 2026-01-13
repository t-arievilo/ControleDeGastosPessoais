using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    public abstract class ServiceBase
    {
    protected readonly IUnitOfWork _unitOfWork;
    
    protected ServiceBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    protected async Task CommitAsync()
    {
        await _unitOfWork.CommitAsync();
    }
    }
}