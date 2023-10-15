using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Services
{
    public interface IDatabaseSynchronizationService
    {
        Task SynchronizeEquipmentsData();
        Task SynchronizeLocationsData();
        Task SynchronizeSuppliersData();
        Task SynchronizeEquipmentTypesData();
    }
}
