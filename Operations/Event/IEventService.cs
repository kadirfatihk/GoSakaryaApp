using GoSakaryaApp.Business.Operations.Area.Dtos;
using GoSakaryaApp.Business.Operations.Event.Dtos;
using GoSakaryaApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Event
{
    public interface IEventService
    {
        Task<ServiceMessage> AddEvent(AddEventDto eventDto);
        Task<EventDto> GetEvent(int id);
        Task<List<EventDto>> GetEventAll();
        Task<ServiceMessage> AdjustDescription(int id, string description);
        Task<ServiceMessage> UpdateEvent(UpdateEventDto eventDto);
        Task<ServiceMessage> DeleteEvent(int id);
    }
}
