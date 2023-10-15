using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.Store
{
    public class EquipmentStore : BaseViewModel
    {
        public List<Equipment> Equipments { get; private set; }
        public ObservableCollection<string> EquipmentIds { get; private set; }
        public ObservableCollection<string> EquipmentNames { get; private set; }
        public ObservableCollection<string> CodeOfManages { get; private set; }
        public EquipmentStore()
        {
            Equipments = new List<Equipment>();
            EquipmentIds = new ObservableCollection<string>();
            EquipmentNames = new ObservableCollection<string>();
            CodeOfManages = new ObservableCollection<string>();
        }
        public void SetEquipment(IEnumerable<Equipment> equipments)
        {
            Equipments = Equipments.ToList();
            EquipmentIds = new ObservableCollection<string>(Equipments.Select(i => i.EquipmentId).OrderBy(s => s));
            EquipmentNames = new ObservableCollection<string>(Equipments.Select(i => i.EquipmentName).OrderBy(s => s));
            CodeOfManages = new ObservableCollection<string>(Equipments.Select(i => i.CodeOfManage).OrderBy(s => s));
        }
    }
}
