using GoSakaryaApp.Data.Entities;
using GoSakaryaApp.Data.Repositories;
using GoSakaryaApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        // Bakım durumunu getiren metot.
        public bool GetMaintenanceState()
        {
            var maintenanceState = _settingRepository.GetById(1).MaintenenceMode;   // Veritabanından bakım modu durumu alınır.

            return maintenanceState;
        }

        // Bakım durumunu değiştiren metot.
        public async Task ToggleMaintenance()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenenceMode = !setting.MaintenenceMode; // Bakım modu durumu tersine çevrilir.

            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw new Exception("Bakım durumu güncellenirken bir hatayla karşılaşıldı.");
            }
        }
    }
}
