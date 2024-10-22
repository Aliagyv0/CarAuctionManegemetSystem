using Microsoft.AspNetCore.Http;


namespace CarAuctionApi.Service.DTOs.Request
{
    public record SliderDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //Microsoft.AspNetCore.Http
        public IFormFile Image { get; set; }
    }
}
