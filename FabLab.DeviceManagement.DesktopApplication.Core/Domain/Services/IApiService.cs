using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.EquipmentTypes;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
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
        Task<IEnumerable<EquipmentDto>> GetEquipmentsRecordsAsync(string yearSelected, string equipmentId, string equipmentTypeId, ECategory? category);
        Task CreateEquipment(CreateEquipmentDto equipment);
        Task FixEquipmentAsync(FixEquipmentDto fixDto);
        Task DeleteEquipmentAsync(string equipmentId);

        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task CreateLocation(LocationDto location);
        Task FixLocationAsync(LocationDto fixDto);
        Task DeleteLocationAsync(string locationId);

        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task CreateSupplier(SupplierDto supplier);
        Task FixSupplierAsync(SupplierDto fixDto);
        Task DeleteSupplierAsync(string supplierName);

        Task<IEnumerable<EquipmentTypeDto>> GetAllEquipmentTypesAsync();
        Task CreateEquipmentType(EquipmentTypeDto equipmentType);
        Task FixEquipmentTypesAsync(EquipmentTypeDto fixDto);
        Task DeleteEquipmentTypeAsync(string equipmentTypeId);
    }
}
