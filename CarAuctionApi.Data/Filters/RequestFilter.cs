using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Data.Filters
{
    public class RequestFilter
    {
        //Filtering
        public string? FilterField { get; set; }
        public string? FilterValue { get; set; }


       //Sorting parametrs
        public string? SortField { get; set; }
        public bool IsDescending { get; set; }


        //Paging parametrs
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;
        [Range(1, int.MaxValue)]

        public int Count { get; set; } = 10;
    }
}
