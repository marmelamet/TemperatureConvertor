using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
using TempApp.Core.Contexts;
using TempApp.Core.Entities.Concrete;
using TempApp.Data.Repositories.Abstract;

namespace TempApp.Data.Repositories.Concrete
{
    public class ConversionRepository<T>(AppDbContext context) : IConversionRepository<T> where T : BaseEntity
    {
        readonly private AppDbContext _context = context;

        public DbSet<T> Table => _context.Set<T>();
        public List<T> GetAll() => [.. _context.Set<T>()];
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
