using GoSakaryaApp.Business.Operations.Comment.Dtos;
using GoSakaryaApp.Business.Types;
using GoSakaryaApp.Data.Entities;
using GoSakaryaApp.Data.Repositories;
using GoSakaryaApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Comment
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CommentEntity> _repository;

        public CommentManager(IUnitOfWork unitOfWork, IRepository<CommentEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ServiceMessage<AddCommentDto>> AddCommentAsync(AddCommentDto commentDto)
        {
            try
            {
                // Yeni yorum nesnesi oluşturur.
                var commentEntity = new CommentEntity
                {
                    Text = commentDto.Text,
                    AreaId = commentDto.AreaId,
                    EventId = commentDto.EventId,
                    CreatedDate = DateTime.UtcNow
                };

                _repository.Add(commentEntity);

                await _unitOfWork.SaveChangesAsync();

                // Eklenen yorumun ID'sini ve oluşturulma tarihini DTO'ya ekler.
                commentDto.Id = commentEntity.Id;
                commentDto.CreatedDate = commentEntity.CreatedDate;

                return new ServiceMessage<AddCommentDto>
                {
                    IsSucceed = true,
                    Message = "Yorum başarıyla eklendi.",
                    Data = commentDto
                };
            }
            catch (Exception)
            {
                return new ServiceMessage<AddCommentDto>
                {
                    IsSucceed = false,
                    Message = "Yorum eklenirken bir hata oluştu."
                };
            }
        }

        public async Task<ServiceMessage> DeleteComment(int id)
        {
            // Silinecek yorumu veritabanından getirir.
            var comment = _repository.GetById(id);

            if (comment is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen yorum bulunamadı."
                };

            _repository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Yorum silinirken bir hatayla karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Yorum başarıyla silindi."
            };
        }

        public async Task<ServiceMessage<List<CommentDto>>> GetCommentsAllAsync()
        {
            // Silinmemiş tüm yorumları, ilişkili alan ve etkinlik bilgileriyle birlikte getirir.
            var comments = await _repository.GetAll(x => !x.IsDeleted)
                .Include(x => x.Area)
                .Include(x => x.Event)
                .Select(x => new CommentDto
                {
                    Id = x.Id,
                    AreaName = x.Area != null ? x.Area.Name : null, // Konum ismini alır (Konum varsa).
                    EventName = x.Event != null ? x.Event.Name : null,  // Etkinlik ismini alır (Etkinlik varsa).
                    Text = x.Text,
                    CreatedDate = x.CreatedDate,
                    AreaId = x.AreaId,
                    EventId = x.EventId
                }).ToListAsync();

            return new ServiceMessage<List<CommentDto>>
            {
                IsSucceed = true,
                Data = comments
            };
        }

        public async Task<ServiceMessage<List<CommentDto>>> GetCommentsByAreaIdAsync(int areaId)
        {
            // Belirtilen alan ID'sine ait silinmemiş yorumları ve ilişkili alan bilgisini getirir.
            var comments = await _repository.GetAll(c => c.AreaId == areaId && !c.IsDeleted)
                .Include(c => c.Area)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    AreaName = c.Area.Name,
                    Text = c.Text,
                    CreatedDate = c.CreatedDate,
                    AreaId = c.AreaId,
                    EventId = c.EventId,
                })
                .ToListAsync();

            return new ServiceMessage<List<CommentDto>>
            {
                IsSucceed = true,
                Data = comments
            };
        }

        public async Task<ServiceMessage<List<CommentDto>>> GetCommentsByEventIdAsync(int eventId)
        {
            var comments = await _repository.GetAll(c => c.EventId == eventId && !c.IsDeleted)
                .Include(c => c.Event)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    EventName = c.Event.Name,
                    Text = c.Text,
                    CreatedDate = c.CreatedDate,
                    EventId = c.EventId,
                    AreaId = c.AreaId,
                })
                .ToListAsync();

            return new ServiceMessage<List<CommentDto>>
            {
                IsSucceed = true,
                Data = comments
            };
        }
    }
}
