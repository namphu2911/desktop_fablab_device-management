using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments
{
    public class EquipmentTypeDto
    {
        public string Id { get; set; }
        public string Picture { get; set; }
        public string Value { get; set; }
        public ECategory Category { get; set; }
        public EquipmentTypeDto(string id, string picture, string value, ECategory category)
        {
            Id = id;
            Picture = picture;
            Value = value;
            Category = category;
        }
    }
}
