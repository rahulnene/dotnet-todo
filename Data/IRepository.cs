using dotnet_todo.Models;

namespace dotnet_todo.Data
{
    public interface IRepository<T> where T: class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<int> SaveChanges();
    }
}
