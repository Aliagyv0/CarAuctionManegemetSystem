using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class NewsImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public Guid NewsId { get; set; }
        public virtual News News { get; set; }
    }
}
