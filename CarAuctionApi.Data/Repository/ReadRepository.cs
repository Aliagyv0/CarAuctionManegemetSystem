using CarAuctionApi.Core.Models.BaseEntities;
using CarAuctionApi.Data.Context;
using CarAuctionApi.Data.Filters;
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


        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, RequestFilter? filter)
        {
            var query = _dbSet.Where(predicate);
            if (filter != null)
            {
                //Filtering
                if(!string.IsNullOrEmpty(filter.FilterField) && !string.IsNullOrEmpty(filter.FilterValue))
                {
                    query = query.Where(e=>EF.Property<string>(e,filter.FilterField) == filter.FilterValue);
                }


                //sorting
                if (!string.IsNullOrEmpty(filter.SortField))
                {
                    //if (!filter.IsDescending)
                    //{
                    //    query = query.OrderBy(e => EF.Property<object>(e, filter.SortField));
                    //}
                    //else
                    //{
                    //    query = query.OrderByDescending(e => EF.Property<object>(e, filter.SortField));
                    //}

                    query = filter.IsDescending ? query.OrderByDescending(e => EF.Property<object>(e, filter.SortField)) : query.OrderBy(e => EF.Property<object>(e, filter.SortField));
                }
                //paging
                query = query.Skip((filter.Page - 1) * filter.Count).Take(filter.Count);
            }
            return query;
        }
             
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.AnyAsync(predicate);
    }
}
