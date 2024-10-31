using CarAuctionApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.DTOs.Response
{
    public record NewsGetDto()
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryGetDto Category { get; set; }
        public string Thesis { get; set; }
        public ICollection<NewsImageGetDto> NewsImages { get; set; }
        public ICollection<TagGetDto> Tags { get; set; }
    }

}
