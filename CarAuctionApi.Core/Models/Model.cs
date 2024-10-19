using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
