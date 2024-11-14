using GoSakaryaApp.Business.Operations.Event;
using GoSakaryaApp.Business.Operations.Event.Dtos;
using GoSakaryaApp.Business.Operations.EvenTicket.Dtos;
using GoSakaryaApp.Business.Types;
using GoSakaryaApp.WebApi.Filters;
using GoSakaryaApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GoSakaryaApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventId = await _eventService.GetEvent(id);

            if (eventId is null)
                return NotFound();
            else
                return Ok(eventId);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventAll()
        {
            var events = await _eventService.GetEventAll();

            return Ok(events);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        //[TimeControlFilter]
        public async Task<IActionResult> AddEvent(AddEventRequestModel requestModel)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // Request model'den DTO'ya dönüştürme.
            var addEventDto = new AddEventDto
            {
                Name = requestModel.Name,
                Location = requestModel.Location,
                Artist = requestModel.Artist,
                TicketPrice = requestModel.TicketPrice,
                Description = requestModel.Description,
                EventDate = requestModel.EventDate,
                EventDuration = requestModel.EventDuration,
                TicketCapacity = requestModel.TicketCapacity,
            };

            var result = await _eventService.AddEvent(addEventDto);

            if(result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpPatch("{id}/description")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdjustDescription(int id,  string description)
        {
            var result = await _eventService.AdjustDescription(id, description);

            if(!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventRequestModel requestModel)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != requestModel.Id)
                return BadRequest(new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Id'ler uyuşmuyor."
                });

            var updateEventDto = new UpdateEventDto
            {
                Id = requestModel.Id,
                Description = requestModel.Description,
                EventDate = requestModel.EventDate,
                EventDuration = requestModel.EventDuration,
                Name = requestModel.Name,
                TicketCapacity = requestModel.TicketCapacity,
                Artist = requestModel.Artist,
                Location = requestModel.Location,
                TicketPrice = requestModel.TicketPrice,
            };

            var result = await _eventService.UpdateEvent(updateEventDto);

            if(result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteEvent(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }
    }
}
