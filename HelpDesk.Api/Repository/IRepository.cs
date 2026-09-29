namespace HelpDesk.Api.Repository
{
    public interface IRepository<T>
    {
        Task<T> Get(int id, CancellationToken token = default);
        Task<List<T>> GetAll(CancellationToken token = default);
        Task<bool> Add(T entity, CancellationToken token = default);
        Task<bool> Delete(int id, CancellationToken token = default);
        Task<bool> Delete(T entity, CancellationToken token = default);
        Task<bool> Update(T entity, CancellationToken token = default);

    }
}
