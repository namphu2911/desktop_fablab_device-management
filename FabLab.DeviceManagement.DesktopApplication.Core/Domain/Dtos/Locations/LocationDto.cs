using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations
{
    public class LocationDto
    {
        public string Id { get; set; }
        public string Note { get; set; }
        public LocationDto(string id, string note)
        {
            Id = id;
            Note = note;
        }
    }
}
