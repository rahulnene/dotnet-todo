
using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo.Data
{
    public class Repository<T> : IRepository<T> where T : class, IActorInterface
    {
        protected readonly CharacterDbContext _context;
        public Repository(CharacterDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            _context.Add(entity);
            await SaveChanges();
        }

        public async Task Delete(int id)
        {
            var existingEntity = await Get(id);
            if (existingEntity != null)
            {
                _context.Remove(existingEntity);
                await SaveChanges();
            }
            else
            {
                throw new Exception($"Entity with id {id} not found.");
            }
        }

        public async Task<T> Get(int id)
        {
            var result =  await _context.FindAsync<T>(id);
            return result ?? throw new Exception($"Entity with id {id} not found.");
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<int> SaveChanges()
        {            
            return await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var existingEntity = await Get(entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await SaveChanges();
            }
            else
            {
                throw new Exception($"Entity with id {entity.Id} not found.");
            }
        }


    }
}
