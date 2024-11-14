using GoSakaryaApp.Business.Operations.EvenTicket;
using GoSakaryaApp.Business.Operations.EvenTicket.Dtos;
using GoSakaryaApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSakaryaApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTicketsController : ControllerBase
    {
        private readonly IEventTicketService _eventTicketService;

        public EventTicketsController(IEventTicketService eventTicketService)
        {
            _eventTicketService = eventTicketService;
        }

        // Bilet satın almak için HTTP POST metodu.
        [HttpPost("buy")]
        [Authorize(Roles ="Visitor")]
        public async Task<IActionResult> BuyTicket(BuyTicketRequestModel requestModel)
        {
            // Model doğrulama kontrolü.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Request model'den DTO'ya dönüştürme.
            var buyTicketDto = new BuyTicketDto
            {
                EventId = requestModel.EventId,
                UserId = requestModel.UserId,
                TicketCount = requestModel.TicketCount
            };

            // Bilet satın alma işlemi.
            var result = await _eventTicketService.BuyTicket(buyTicketDto);

            if(result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        // Kullanıcı ID'sine göre biletleri getirmek için HTTP GET metodu.
        [HttpGet("user/{userId}")] 
        [Authorize(Roles ="Visitor")]
        public async Task<IActionResult> GetTicketsByUserId(int userId)
        {

            var result = await _eventTicketService.GetTicketByUser(userId);


            if (result.IsSucceed)

                return Ok(result);

            else

                return BadRequest(result.Message);

        }
    }
}
