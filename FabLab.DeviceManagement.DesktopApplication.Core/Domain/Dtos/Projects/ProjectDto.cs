using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Borrowings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Projects
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public List<BorrowDto> Borrows { get; set; }
        public ProjectDto(string projectName, DateTime startDay, DateTime endDay, string description, bool approved, List<BorrowDto> borrows)
        {
            ProjectName = projectName;
            StartDay = startDay;
            EndDay = endDay;
            Description = description;
            Approved = approved;
            Borrows = borrows;
        }
    }
}
