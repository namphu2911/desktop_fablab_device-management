using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.Store
{
    public class SupplierStore : BaseViewModel
    {
        public List<SupplierDto> Suppliers { get; private set; }
        public ObservableCollection<string> SupplierNames { get; private set; }
        public ObservableCollection<string> Addresses { get; private set; }
        public ObservableCollection<string> PhoneNumbers { get; private set; }
        public SupplierStore()
        {
            Suppliers = new List<SupplierDto>();
            SupplierNames = new ObservableCollection<string>();
            Addresses = new ObservableCollection<string>();
            PhoneNumbers = new ObservableCollection<string>();
        }
        public void SetSupplier(IEnumerable<SupplierDto> suppliers)
        {
            Suppliers = suppliers.ToList();
            SupplierNames = new ObservableCollection<string>(Suppliers.Select(i => i.SupplierName).OrderBy(s => s));
            Addresses = new ObservableCollection<string>(Suppliers.Select(i => i.Address).OrderBy(s => s));
            PhoneNumbers = new ObservableCollection<string>(Suppliers.Select(i => i.PhoneNumber).OrderBy(s => s));
        }
    }
}
