using GoSakaryaApp.Business.Operations.Comment.Dtos;
using GoSakaryaApp.Business.Operations.Event.Dtos;
using GoSakaryaApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Comment
{
    public interface ICommentService
    {
        Task<ServiceMessage<AddCommentDto>> AddCommentAsync(AddCommentDto commentDto);
        Task<ServiceMessage<List<CommentDto>>> GetCommentsByAreaIdAsync(int areaId); // Alan ID'sine göre yorumları getir
        Task<ServiceMessage<List<CommentDto>>> GetCommentsByEventIdAsync(int eventId); // Etkinlik ID'sine göre yorumları getir
        Task<ServiceMessage<List<CommentDto>>> GetCommentsAllAsync();
        Task<ServiceMessage> DeleteComment(int id);
    }
}
