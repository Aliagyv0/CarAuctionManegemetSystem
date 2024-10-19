using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class CarAuctionDetail :BaseEntity
    {
        public double InitialPrice { get; set; }
        public double WinningPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
