using GoSakaryaApp.Business.Operations.Event.Dtos;
using GoSakaryaApp.Business.Operations.EvenTicket.Dtos;
using GoSakaryaApp.Business.Types;
using GoSakaryaApp.Data.Entities;
using GoSakaryaApp.Data.Repositories;
using GoSakaryaApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.EvenTicket
{
    public class EventTicketManager : IEventTicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<EventTicketEntity> _eventTicketRepository;
        private readonly IRepository<EventEntity> _eventRepository;
        private readonly IRepository<UserEntity> _userRepository;

        public EventTicketManager(IUnitOfWork unitOfWork,
                                  IRepository<EventTicketEntity> eventTicketRepository,
                                  IRepository<EventEntity> eventRepository,
                                  IRepository<UserEntity> userRepository)
        {
            _unitOfWork = unitOfWork;
            _eventTicketRepository = eventTicketRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceMessage> BuyTicket(BuyTicketDto buyTicketDto)
        {
            // Etkinlik bilgileri ve ilişkili biletler veritabanından getirtilir.
            var eventEntity = await _eventRepository
                            .GetAll(x => x.Id == buyTicketDto.EventId)
                            .Include(x => x.EventTickets)
                            .FirstOrDefaultAsync();

            // Etkinlik bulunamazsa hata mesajı döndürülür.
            if (eventEntity is null)
                return new ServiceMessage { IsSucceed = false, Message = "Etkinlik bulunamadı." };

            var existingTicket = await _eventTicketRepository
                                .GetAll(x => x.UserId == buyTicketDto.UserId && x.EventId == buyTicketDto.EventId)
                                .FirstOrDefaultAsync();

            // Veritabanında böyle bir kullanıcı olup olmadığı kontrol edilir.
            var userEntity = await _userRepository.GetAll(x => x.Id == buyTicketDto.UserId).FirstOrDefaultAsync();

            if (userEntity is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Kullanıcı bulunamadı."
                };

            if (existingTicket != null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu etkinlik için zaten bir biletiniz var."
                };

            // Kalan bilet sayısı hesaplanır.
            int ticketCapacity = eventEntity.TicketCapacity - buyTicketDto.TicketCount;

            // En fazla 1 bilet
            if (buyTicketDto.TicketCount > 1)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "En fazla 1 adet bilet alabilirsiniz."
                };
            }

            if (buyTicketDto.TicketCount <= 0)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "1 bilet alabilirsiniz."
                };

            // Yeterli bilet yoksa hata mesajı döndürülür.
            if (ticketCapacity < buyTicketDto.TicketCount)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Yeterli bilet yok."
                };

            // Bilet kalmamışsa hata mesajı döndürülür.
            if (ticketCapacity <= 0)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bilet kalmamıştır."
                };


            try
            {
                // İstenen sayıda bilet oluşturulup veritabanına eklenir.
                for (int i = 0; i < buyTicketDto.TicketCount; i++)
                {
                    var ticket = new EventTicketEntity
                    {
                        EventId = buyTicketDto.EventId,
                        UserId = buyTicketDto.UserId,
                        PurchaseDate = DateTime.Now
                    };

                    _eventTicketRepository.Add(ticket);


                }

                eventEntity.TicketCapacity -= buyTicketDto.TicketCount;   // Kalan bilet sayısı azaltılır.
                _eventRepository.Update(eventEntity);   // Etkinlik bilgileri güncellenir. (Kalan bilet sayısı)

                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception)
            {
                return new ServiceMessage { IsSucceed = false, Message = "Bilet satın alınırken bir hata oluştu." };
            }

            return new ServiceMessage { IsSucceed = true, Message = "Bilet başarıyla satın alındı." };
        }

        public async Task<ServiceMessage<List<EventDto>>> GetTicketByUser(int userId)
        {
            try
            {
                // Kullanıcının biletleri ve ilişkili etkinlik bilgileri veritabanından getirilir.
                var tickets = await _eventTicketRepository.GetAll(t => t.UserId == userId)
                    .Include(t => t.Event)
                    .Select(t => new EventDto
                    {
                        Id = t.Event.Id,
                        Name = t.Event.Name,
                        Location = t.Event.Location,
                        Artist = t.Event.Artist,
                        Description = t.Event.Description,
                        EventDate = t.Event.EventDate,
                        EventDuration = t.Event.EventDuration,
                        TicketPrice = t.Event.TicketPrice
                    })
                    .ToListAsync();

                return new ServiceMessage<List<EventDto>>
                {
                    IsSucceed = true,
                    Message = "Biletler başarıyla getirildi.",
                    Data = tickets
                };
            }
            catch (Exception ex)
            {
                return new ServiceMessage<List<EventDto>>
                {
                    IsSucceed = false,
                    Message = "Biletleri getirirken bir hata oluştu."
                };
            }
        }
    }
}