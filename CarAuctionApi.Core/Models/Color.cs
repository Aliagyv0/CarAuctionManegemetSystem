using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
