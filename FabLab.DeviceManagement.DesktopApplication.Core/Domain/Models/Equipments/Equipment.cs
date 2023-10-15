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
        public string CodeOfManage { get; set; }
        public Equipment(string equipmentId, string equipmentName, string codeOfManage)
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            CodeOfManage = codeOfManage;
        }
    }
}
