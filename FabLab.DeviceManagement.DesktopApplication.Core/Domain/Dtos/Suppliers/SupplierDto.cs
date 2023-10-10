using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers
{
    public class SupplierDto
    {
        public string SupplierName { get; set; }
        public string Address { get; set; } 
        public string PhoneNumber { get; set; }
        public SupplierDto(string supplierName, string address, string phoneNumber)
        {
            SupplierName = supplierName;    
            Address = address;
            PhoneNumber = phoneNumber;  
        }
            
    }
}
