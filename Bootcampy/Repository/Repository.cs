using Bootcampy.Data;
using Bootcampy.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bootcampy.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<T>> ReadAllAsync()
        {            
            return await context.Set<T>().ToListAsync();
        }
        public async Task<List<T>> ReadAllAsync(Expression<Func<T, bool>> filter)
        {
            return await context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> ReadOneAsync(int id)
        {            
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await context.Set<T>().FindAsync(id);
            if (student == null)
                throw new ArgumentException(nameof(student));

            context.Set<T>().Remove(student);
            await context.SaveChangesAsync();
        }
    }
}
