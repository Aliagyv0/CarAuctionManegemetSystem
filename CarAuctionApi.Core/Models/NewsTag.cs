using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class NewsTag : BaseEntity
    {
        public Guid NewsID { get; set; }
        public Guid TagId { get; set; }
        public virtual News News { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
