using GoSakaryaApp.Business.Operations.Area.Dtos;
using GoSakaryaApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Area
{
    public interface IAreaService
    {
        Task<ServiceMessage> AddArea(AddAreaDto area);  // Konum ekleme metodu.
        Task<AreaDto> GetArea(int id);  // Belirli bir id'li konumu getiren metot.
        Task<List<AreaDto>> GetAllAreas();  // Bütün konumları getiren metot.
        Task<ServiceMessage> AdjustDescription(int id, string description);     // Konumun, açıklama özelliğinde güncelleme yapan metot.
        Task<ServiceMessage> UpdateArea(UpdateAreaDto areaDto);     // Konumda güncelleme yapan metot.
        Task<ServiceMessage> DeleteArea(int id);    // Belirli bir id'li konumu silen metot.
    }
}
