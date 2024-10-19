using CarAuctionApi.Core.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Core.Models
{
    public class News : BaseEntity
    {
     public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string Thesis { get; set; }
        public string Text { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
        public virtual ICollection<NewsImage> NewsImages { get; set; }

    }
}
