using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Data.Repository.Interfaces
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        bool Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        bool Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<bool> Delete(string id);
        Task<bool> SoftDelete(string id);
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
