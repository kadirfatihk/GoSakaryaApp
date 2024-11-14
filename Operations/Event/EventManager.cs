using GoSakaryaApp.Business.Operations.Area.Dtos;
using GoSakaryaApp.Business.Operations.Event.Dtos;
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

namespace GoSakaryaApp.Business.Operations.Event
{
    public class EventManager : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<EventEntity> _repository;

        public EventManager(IUnitOfWork unitOfWork, IRepository<EventEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ServiceMessage> AddEvent(AddEventDto eventDto)
        {
            try
            {
                if (_repository.GetAll(x => x.Name.ToLower() == eventDto.Name.ToLower()).Any())
                {
                    return new ServiceMessage { IsSucceed = false, Message = "Bu etkinlik zaten mevcut." };
                }

                var eventEntity = new EventEntity
                {
                    Name = eventDto.Name,
                    Location = eventDto.Location,
                    Artist = eventDto.Artist,
                    Description = eventDto.Description,
                    TicketPrice = eventDto.TicketPrice,
                    EventDate = eventDto.EventDate,
                    EventDuration = eventDto.EventDuration,
                    TicketCapacity = eventDto.TicketCapacity,
                };

                _repository.Add(eventEntity);

                await _unitOfWork.SaveChangesAsync(); 

                return new ServiceMessage { IsSucceed = true, Message = "Etkinlik başarıyla kaydedildi." };
            }
            catch (Exception)
            {
                
                return new ServiceMessage { IsSucceed = false, Message = "Etkinlik kaydedilirken bir hata oluştu." }; // Kullanıcıya genel bir mesaj
            }
        }

        public async Task<List<EventDto>> GetEventAll()
        {
            try
            {
                var events = await _repository.GetAll()
                    .Include(x => x.EventTickets)
                    .ToListAsync();

                return events.Select(x => new EventDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Artist = x.Artist,
                    Description = x.Description,
                    EventDate = x.EventDate,
                    EventDuration = x.EventDuration,
                    Location = x.Location,
                    TicketPrice= x.TicketPrice,
                    TicketCapacity = x.TicketCapacity,
                }).ToList();
            }
            catch (Exception)
            {               
                return new List<EventDto>(); 
            }
        }

        public async Task<ServiceMessage> AdjustDescription(int id, string description)
        {
            var events = _repository.GetById(id);

            if (events is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu id'ye sahip etkinlik bulunamadı."
                };

            // Açıklamayı günceller.
            events.Description = description;

            _repository.Update(events);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Açıklama değiştirilirken bir hata ile karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Bilet sayısı başarıyla değiştirildi."
            };
        }

        public async Task<ServiceMessage> DeleteEvent(int id)
        {
            var events = _repository.GetById(id);

            if (events is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen etkinlik bulunamadı."
                };

            _repository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Silme işlemi sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Etkinlik başarıyla silindi."
            };
        }

        public async Task<EventDto> GetEvent(int id)
        {
            try
            {
                var eventEntity = await _repository.GetAll(x => x.Id == id)
                    .Include(x => x.EventTickets)
                    .FirstOrDefaultAsync();

                if (eventEntity == null)
                    return null;


                return new EventDto
                {
                    Id = eventEntity.Id,
                    Name = eventEntity.Name,
                    Location = eventEntity.Location,
                    Artist = eventEntity.Artist,
                    TicketPrice = eventEntity.TicketPrice,
                    Description = eventEntity.Description,
                    EventDate = eventEntity.EventDate,
                    EventDuration = eventEntity.EventDuration,
                    TicketCapacity = eventEntity.TicketCapacity,
                };
            }
            catch (Exception ex)
            {
                // Loglama
                return null; // veya hata mesajı döndüren bir result
            }
        }

        public async Task<ServiceMessage> UpdateEvent(UpdateEventDto eventDto)
        {
            // Güncellenecek etkinliği veritabanından getirir.
            var existingEvent = _repository.GetById(eventDto.Id);

            if (existingEvent is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Etkinlik bulunamadı."
                };

            // Etkinlik bilgilerini günceller.
            existingEvent.Name = eventDto.Name;
            existingEvent.Location = eventDto.Location;
            existingEvent.Artist = eventDto.Artist;
            existingEvent.TicketPrice = eventDto.TicketPrice;
            existingEvent.Description = eventDto.Description;
            existingEvent.EventDate = eventDto.EventDate;
            existingEvent.EventDuration = eventDto.EventDuration;
            existingEvent.TicketCapacity = eventDto.TicketCapacity;

            _repository.Update(existingEvent);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Güncelleme sırasında bir hata ile karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Etkinlik başarıyla güncellendi."
            };
        }
    }
}