using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments
{
    public class Equipment
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string CodeOfManager { get; set; }
        public Equipment(string equipmentId, string equipmentName, string codeOfManager)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            CodeOfManager = codeOfManager;
        }
    }
}
