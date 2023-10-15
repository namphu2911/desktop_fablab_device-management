using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Services
{
    public interface IApiService
    {
        //Equipment
        Task<IEnumerable<EquipmentDto>> GetAllEquipmentsAsync();
        Task CreateEquipment(CreateEquipmentDto equipment);
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task CreateLocation(LocationDto location);
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task CreateSupplier(SupplierDto supplier);
        Task<IEnumerable<EquipmentTypeDto>> GetAllEquipmentTypesAsync();
        Task CreateEquipmentType(EquipmentTypeDto equipmentType);
    }
}
