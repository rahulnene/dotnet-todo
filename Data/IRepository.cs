using dotnet_todo.Models;

namespace dotnet_todo.Data
{
    public interface IRepository<T> where T: class, IActor
    {
        Task<T> Get(int id);
        Task<List<T>?> GetAll();
        Task<int> Add(T entity);
        Task Update<U>(U entity) where U:class,IActor;
        Task Delete(int id);
    }
}
