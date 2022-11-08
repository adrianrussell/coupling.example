using LowCouplingAPI.Application;

namespace LowCouplingAPI.Test.Controllers;

public class RepositoryBaseMock : IRepositoryBase
{
    public int CreateCalledCounter { get; private set; }

    public int UpdateCalledCounter { get; private set; }

    public Task Create<T>(T entity)
    {
        CreateCalledCounter++;
        return Task.CompletedTask;
    }

    public Task Update<T>(T entity)
    {
        UpdateCalledCounter++;
        return Task.CompletedTask;
    }
}