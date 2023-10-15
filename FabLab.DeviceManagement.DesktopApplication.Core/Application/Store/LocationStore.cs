using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.Store
{
    public class LocationStore : BaseViewModel
    {
        public List<LocationDto> Locations { get; private set; }
        public ObservableCollection<string> LocationIds { get; private set; }
        public ObservableCollection<string> Notes { get; private set; }
        public LocationStore()
        {
            Locations = new List<LocationDto>();
            LocationIds = new ObservableCollection<string>();
            Notes = new ObservableCollection<string>();
        }
        public void SetLocation(IEnumerable<LocationDto> locations)
        {
            Locations = locations.ToList();
            LocationIds = new ObservableCollection<string>(Locations.Select(i => i.LocationId).OrderBy(s => s));
            Notes = new ObservableCollection<string>(Locations.Select(i => i.Note).OrderBy(s => s));
        }
    }
}
