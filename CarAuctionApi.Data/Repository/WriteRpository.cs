using CarAuctionApi.Core.Models.BaseEntities;
using CarAuctionApi.Data.Context;
using CarAuctionApi.Data.Exceptions;
using CarAuctionApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Data.Repository
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly CarAuctionDbContext _context;
        private readonly DbSet<T> _dbSet;
        public WriteRepository(CarAuctionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
            => await _dbSet.AddRangeAsync(entities);

        public bool Update(T entity)
        {
            EntityEntry entityEntry = _dbSet.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
            => _dbSet.UpdateRange(entities);

        public bool Delete(T entity)
        {
            EntityEntry entityEntry = _dbSet.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<T> entities)
            => _dbSet.RemoveRange(entities);

        public async Task<bool> Delete(string id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id.ToString() == id);
            if (entity == null)
                throw new EntityNotFoundException("Entity isn`t found");

            EntityEntry entityEntry = _dbSet.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> SoftDelete(string id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id.ToString() == id);
            if (entity == null)
                throw new EntityNotFoundException("Entity isn`t found");

            entity.IsDeleted = true;
            return true;
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public int SaveChanges()
            => _context.SaveChanges();
    }
}
