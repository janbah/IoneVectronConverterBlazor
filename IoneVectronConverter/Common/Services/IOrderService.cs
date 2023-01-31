using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Services;

public class IOrderService
{
    private readonly IRepository<Order> _repository;

    public IOrderService(IRepository<Order> repository)
    {
        _repository = repository;
    }
    
    
}