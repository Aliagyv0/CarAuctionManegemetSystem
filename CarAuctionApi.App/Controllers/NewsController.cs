using CarAuctionApi.Data.Filters;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Services;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionApi.App.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] NewsDto dto)
        {
            var result = await _newsService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] NewsDto dto)
        {
            var result = await _newsService.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _newsService.DeleteAsync(id);
            return StatusCode(result.StatusCode);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _newsService.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter filter)
        {
            var result = await _newsService.GetAllAsync(filter);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(NewsImageDto dto)
        {
            var result = await _newsService.AddImage(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(string id)
        {
          var result = await _newsService.DeleteImage(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
