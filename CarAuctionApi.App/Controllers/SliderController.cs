using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionApi.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SliderController : ControllerBase
    {
        private readonly ISliderServices _sliderServices;

        public SliderController(ISliderServices sliderServices)
        {
            _sliderServices = sliderServices;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] SliderDto dto)
        {
            var result = await _sliderServices.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] SliderDto dto)
        {
            var result = await _sliderServices.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _sliderServices.DeleteAsync(id);
            return StatusCode(result.StatusCode);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _sliderServices.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sliderServices.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
    }
}
