using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments
{
    public class EquipmentDto
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public DateTime YearOfSupply { get; set; }
        public string CodeOfManage { get; set; }
        public LocationDto Location { get; set; }
        public SupplierDto Supplier { get; set; }
        public EStatus Status { get; set; }
        public EquipmentTypeDto EquipmentType { get; set; }
        public EquipmentDto(string equipmentId, string equipmentName, DateTime yearOfSupply, string codeOfManage, LocationDto location, SupplierDto supplier, EStatus status, EquipmentTypeDto equipmentType)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            YearOfSupply = yearOfSupply;
            CodeOfManage = codeOfManage;
            Location = location;
            Supplier = supplier;
            Status = status;
            EquipmentType = equipmentType;
        }
    }
}
