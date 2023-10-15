using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.Device
{
    public class DeviceEntryViewModel : BaseViewModel
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public DateTime YearOfSupply { get; set; }
        public string CodeOfManage { get; set; }
        public string LocationId { get; set; }
        public string SupplierName { get; set; }
        public EStatus Status { get; set; }
        public string EquipmentTypeId { get; set; }
        public string EquipmentTypeName { get; set; }
        public DeviceEntryViewModel(string equipmentId, string equipmentName, DateTime yearOfSupply, string codeOfManage, string locationId, string supplierName, EStatus status, string equipmentTypeId, string equipmentTypeName)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            YearOfSupply = yearOfSupply;
            CodeOfManage = codeOfManage;
            LocationId = locationId;
            SupplierName = supplierName;
            Status = status;
            EquipmentTypeId = equipmentTypeId;
            EquipmentTypeName = equipmentTypeName;
        }
    }
}
