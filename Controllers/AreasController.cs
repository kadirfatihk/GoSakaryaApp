using GoSakaryaApp.Business.Operations.Area;
using GoSakaryaApp.Business.Operations.Area.Dtos;
using GoSakaryaApp.Business.Types;
using GoSakaryaApp.WebApi.Filters;
using GoSakaryaApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSakaryaApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreasController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await _areaService.GetAllAreas();

            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var area = await _areaService.GetArea(id);

            if (area is null)
                return NotFound();
            else
                return Ok(area);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> AddArea(AddAreaRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Request Model'den DTO'ya dönüştürme.
            var addAreaDto = new AddAreaDto
            {
                Name = requestModel.Name,
                Location = requestModel.Location,
                Description = requestModel.Description,
                History = requestModel.History
            };

            var result = await _areaService.AddArea(addAreaDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpPatch("{id}/description")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdjustDescription(int id, string description)
        {
            var result = await _areaService.AdjustDescription(id, description);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateArea(int id, UpdateAreaRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != requestModel.Id)
                return BadRequest(new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Id'ler uyuşmuyor."
                });

            var updateAreaDto = new UpdateAreaDto
            {
                Id = requestModel.Id,
                Name = requestModel.Name,
                Location = requestModel.Location,
                Description = requestModel.Description,
                History = requestModel.History
            };

            var result = await _areaService.UpdateArea(updateAreaDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var result = await _areaService.DeleteArea(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }
    }
}
