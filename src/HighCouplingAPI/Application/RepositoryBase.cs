namespace HighCouplingAPI.Application
{
    public interface IRepositoryBase
    {
        Task Create<T>(T entity);
        Task Update<T>(T entity);
    }

    public class RepositoryBase :IRepositoryBase
    {
        public Task Create<T>(T entity)
        {
            return Task.CompletedTask;
        }

        public Task Update<T>(T entity)
        {
            return Task.CompletedTask;
        }
    }
}
