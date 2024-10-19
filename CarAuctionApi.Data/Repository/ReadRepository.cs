using CarAuctionApi.Core.Models.BaseEntities;
using CarAuctionApi.Data.Context;
using CarAuctionApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Data.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly CarAuctionDbContext _context;
        private readonly DbSet<T> _dbSet;
        public ReadRepository(CarAuctionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            if (result is null)
                throw new Exception("Entity isn`t found");

            return result;
        }


        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
            => _dbSet.Where(predicate);


        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.AnyAsync(predicate);
    }
}
