
using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo.Data
{
    public class CharacterRepository<T> : IRepository<T> where T : class, IActor
    {
        protected readonly CharacterDbContext _context;
        public CharacterRepository(CharacterDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(T entity)
        {
            _context.Add(entity);
            await SaveChanges();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            try
            {
                var existingEntity = await Get(id);
                _context.Remove(existingEntity);
                await SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception($"Entity with id {id} not found.");
            }
        }

        public async Task<T> Get(int id)
        {
            var result = await _context.FindAsync<T>(id);
            return result ?? throw new Exception($"Entity with id {id} not found.");
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update<U>(U updatedEntity) where U : class, IActor
        {
            var existingEntity = await Get(updatedEntity.Id);
            var entry = _context.Entry(existingEntity);

            foreach (var property in entry.OriginalValues.Properties)
            {
                var updatedValue = typeof(U).GetProperty(property.Name)?.GetValue(updatedEntity);
                if (updatedValue != null)
                {
                    entry.Property(property.Name).CurrentValue = updatedValue;
                }
            }
            await _context.SaveChangesAsync();
        }



    }
}
