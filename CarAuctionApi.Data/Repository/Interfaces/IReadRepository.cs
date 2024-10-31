using CarAuctionApi.Core.Models.BaseEntities;
using CarAuctionApi.Data.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Data.Repository.Interfaces
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate,RequestFilter? filter);
        Task<bool> IsExistAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
