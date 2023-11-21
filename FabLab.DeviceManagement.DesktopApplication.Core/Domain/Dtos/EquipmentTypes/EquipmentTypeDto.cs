using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.EquipmentTypes
{
    public class EquipmentTypeDto
    {
        public string EquipmentTypeId { get; set; }
        public string Picture { get; set; }
        public string EquipmentTypeName { get; set; }
        public ECategory Category { get; set; }
        public EquipmentTypeDto(string equipmentTypeId, string picture, string equipmentTypeName, ECategory category)
        {
            EquipmentTypeId = equipmentTypeId;
            Picture = picture;
            EquipmentTypeName = equipmentTypeName;
            Category = category;
        }
    }
}
