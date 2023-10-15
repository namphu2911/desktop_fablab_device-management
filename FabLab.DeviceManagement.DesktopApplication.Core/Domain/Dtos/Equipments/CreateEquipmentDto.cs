using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments
{
    public class CreateEquipmentDto
    {
        public string equipmentId { get; set; }
        public string equipmentName { get; set; }
        public DateTime yearOfSupply { get; set; }
        public string codeOfManage { get; set; }
        public string locationId { get; set; }
        public string supplierName { get; set; }
        public EStatus status { get; set; }
        public string equipmentTypeId { get; set; }
        public CreateEquipmentDto(string equipmentId, string equipmentName, DateTime yearOfSupply, string codeOfManage, string locationId, string supplierName, EStatus status, string equipmentTypeId)
        {
            this.equipmentId = equipmentId;
            this.equipmentName = equipmentName;
            this.yearOfSupply = yearOfSupply;
            this.codeOfManage = codeOfManage;
            this.locationId = locationId;
            this.supplierName = supplierName;
            this.status = status;
            this.equipmentTypeId = equipmentTypeId;
        }
    }
}
