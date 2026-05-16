using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TEFLY.DAL.Data;
using TEFLY.DAL.Repositories.Interfaces;

namespace TEFLY.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _set.ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _set.FindAsync(id);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _set.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity)
            => await _set.AddAsync(entity);

        public void Update(T entity)
            => _set.Update(entity);

        public void Delete(T entity)
            => _set.Remove(entity);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
