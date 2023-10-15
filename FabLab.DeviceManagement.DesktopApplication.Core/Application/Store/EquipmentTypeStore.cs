using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.Store
{
    public class EquipmentTypeStore : BaseViewModel
    {
        public List<EquipmentTypeDto> EquipmentTypes { get; private set; }
        public ObservableCollection<string> EquipmentTypeIds { get; private set; }
        public ObservableCollection<string> EquipmentTypeNames { get; private set; }
        public EquipmentTypeStore()
        {
            EquipmentTypes = new List<EquipmentTypeDto>();
            EquipmentTypeIds = new ObservableCollection<string>();
            EquipmentTypeNames = new ObservableCollection<string>();
        }
        public void SetEquipmentType(IEnumerable<EquipmentTypeDto> equipmentTypes)
        {
            EquipmentTypes = equipmentTypes.ToList();
            EquipmentTypeIds = new ObservableCollection<string>(EquipmentTypes.Select(i => i.EquipmentTypeId).OrderBy(s => s));
            EquipmentTypeNames = new ObservableCollection<string>(EquipmentTypes.Select(i => i.EquipmentTypeName).OrderBy(s => s));
        }
    }
}
