using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations
{
    public class LocationDto
    {
        public string LocationId { get; set; }
        public string Note { get; set; }
        public LocationDto(string locationId, string note)
        {
            LocationId = locationId;
            Note = note;
        }
    }
}
