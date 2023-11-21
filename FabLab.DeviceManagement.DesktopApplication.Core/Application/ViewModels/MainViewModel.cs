using CommunityToolkit.Mvvm.Input;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.Device;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDatabaseSynchronizationService _databaseSynchronizationService;
        public DeviceManagementNavigationViewModel DeviceManagementNavigation { get; set; }
        public ICommand LoadDataStoreCommand { get; set; }
        public MainViewModel(IDatabaseSynchronizationService databaseSynchronizationService, DeviceManagementNavigationViewModel deviceManagementNavigation)
        {
            _databaseSynchronizationService = databaseSynchronizationService;
            DeviceManagementNavigation = deviceManagementNavigation;
            LoadDataStoreCommand = new RelayCommand(LoadDataStoreAsync);
        }

        private async void LoadDataStoreAsync()
        {
            await Task.WhenAll(
                _databaseSynchronizationService.SynchronizeLocationsData(),
                _databaseSynchronizationService.SynchronizeEquipmentsData(),
                _databaseSynchronizationService.SynchronizeEquipmentTypesData(),
                _databaseSynchronizationService.SynchronizeSuppliersData()
                );
        }
    }
}
