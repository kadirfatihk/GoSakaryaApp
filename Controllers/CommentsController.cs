using GoSakaryaApp.Business.Operations.Comment;
using GoSakaryaApp.Business.Operations.Comment.Dtos;
using GoSakaryaApp.Business.Operations.Event;
using GoSakaryaApp.Business.Types;
using GoSakaryaApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSakaryaApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCommentsAllAsync()
        {
            var result = await _commentService.GetCommentsAllAsync();

            if (result.IsSucceed)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("area/{areaId}")]
        public async Task<IActionResult> GetCommentsByAreaId(int areaId)
        {
            var result = await _commentService.GetCommentsByAreaIdAsync(areaId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetCommentsByEventId(int eventId)
        {
            var result = await _commentService.GetCommentsByEventIdAsync(eventId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("area/{areaId}")]
        [Authorize(Roles ="Visitor")]
        public async Task<IActionResult> AddCommentToArea(int areaId, AddCommentsRequestModel model)
        {
            var addCommentDto = new AddCommentDto
            {
                Text = model.Text,
                AreaId = areaId
            };

            var result = await _commentService.AddCommentAsync(addCommentDto);
            if (result.IsSucceed)
            {
                return CreatedAtAction(nameof(AddCommentToArea), result);
            }
            return BadRequest(result);
        }

        [HttpPost("event/{eventId}")]
        [Authorize(Roles ="Visitor")]
        public async Task<IActionResult> AddCommentToEvent(int eventId, [FromBody] AddCommentsRequestModel model)
        {
            var addCommentDto = new AddCommentDto
            {
                Text = model.Text,
                EventId = eventId
            };

            var result = await _commentService.AddCommentAsync(addCommentDto);
            if (result.IsSucceed)
            {
                return CreatedAtAction(nameof(AddCommentToEvent), result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteComment(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }
    }
}
