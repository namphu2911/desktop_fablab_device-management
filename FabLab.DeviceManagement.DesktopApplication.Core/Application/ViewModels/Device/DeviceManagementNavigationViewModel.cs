using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.Device
{
    public class DeviceManagementNavigationViewModel
    { 
        public DeviceManagementViewModel DeviceManagement { get; set; }
        public EquipmentTypeViewModel EquipmentType { get; set; }
        public MiscellaneousDataViewModel MiscellaneousData { get; set; }
        public DeviceManagementNavigationViewModel(DeviceManagementViewModel deviceManagement, EquipmentTypeViewModel equipmentType, MiscellaneousDataViewModel miscellaneousData)
        {
            DeviceManagement = deviceManagement;
            EquipmentType = equipmentType;
            MiscellaneousData = miscellaneousData;
        }
    }
}
