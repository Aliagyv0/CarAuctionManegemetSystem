using CarAuctionApi.Data.Filters;
using CarAuctionApi.Service.DTOs.Request;
using CarAuctionApi.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionApi.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServices;

        public CategoryController(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CategoryDto dto)
        {
            var result = await _categoryServices.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] CategoryDto dto)
        {
            var result = await _categoryServices.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _categoryServices.DeleteAsync(id);
            return StatusCode(result.StatusCode);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _categoryServices.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestFilter filter)
        {
            var result = await _categoryServices.GetAllAsync(filter);
            return StatusCode(result.StatusCode, result);
        }                    
    }
}
