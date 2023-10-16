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
        public DeviceManagementViewModel DeviceManagement { get; set; }
        public ICommand LoadDataStoreCommand { get; set; }
        public MainViewModel(IDatabaseSynchronizationService databaseSynchronizationService, DeviceManagementViewModel deviceManagement)
        {
            _databaseSynchronizationService = databaseSynchronizationService;
            DeviceManagement = deviceManagement;
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
