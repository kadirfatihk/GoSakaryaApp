using GoSakaryaApp.Business.Operations.Area.Dtos;
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

namespace GoSakaryaApp.Business.Operations.Area
{
    public class AreaManager : IAreaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AreaEntity> _repository;

        public AreaManager(IUnitOfWork unitOfWork, IRepository<AreaEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ServiceMessage> AddArea(AddAreaDto area)
        {
            // Aynı isimde alan olup olmadığını kontrol eder. (Büyük/küçük harf duyarsız)
            var hasArea = _repository.GetAll(x => x.Name.ToLower() == area.Name.ToLower()).Any();

            if (hasArea)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu yer zaten mevcut."
                };

            // Yeni konum nesnesi oluşturur.
            var areaEntity = new AreaEntity
            {
                Name = area.Name,
                Location = area.Location,
                Description = area.Description,
                History = area.History,
            };

            _repository.Add(areaEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Yer kaydı sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Yer başarıyla kayıt edildi."
            };
        }

        public async Task<ServiceMessage> AdjustDescription(int id, string description)
        {
            // Konumu veritabanından getirir.
            var area = _repository.GetById(id);

            if (area is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu id ile eşleşen konum bulunamadı."
                };

            // Açıklamayı günceller.
            area.Description = description;

            _repository.Update(area);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Açıklama değiştirilirken bir hatayla karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Açıklama başarıyla değiştirildi."
            };
        }

        public async Task<ServiceMessage> DeleteArea(int id)
        {
            // Silinecek alanı veritabanından getirir.
            var area = _repository.GetById(id);

            if (area is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen konum bulunamadı."
                };

            _repository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw new Exception("Konum silinirken bir hatayla karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Konum başarıyla silindi."
            };
        }

        public async Task<List<AreaDto>> GetAllAreas()
        {
            var areas = await _repository.GetAll()
                .Select(x => new AreaDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    Description = x.Description,
                    History = x.History,
                }).ToListAsync();    // LIST'E BİR DAHA BAK

            return areas;
        }

        public async Task<AreaDto> GetArea(int id)
        {
            var area = await _repository.GetAll(x => x.Id == id)
                .Select(x => new AreaDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    Description = x.Description,
                    History = x.History,
                }).FirstOrDefaultAsync();

            return area;
        }

        public async Task<ServiceMessage> UpdateArea(UpdateAreaDto areaDto)
        {
            var existingArea = _repository.GetById(areaDto.Id);

            if (existingArea is null)
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Konum bulunamadı."
                };

            existingArea.Name = areaDto.Name;
            existingArea.Description = areaDto.Description;
            existingArea.History = areaDto.History;
            existingArea.Location = areaDto.Location;

            _repository.Update(existingArea);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw new Exception("Güncelleme sırasında hatayla karşılaşıldı.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Başarıyla güncellendi."
            };
        }
    }
}
