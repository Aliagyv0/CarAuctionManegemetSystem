using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class Tag :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
}
