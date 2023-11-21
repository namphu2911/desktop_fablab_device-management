using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.Store;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Exceptions;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.Device
{
    public class MiscellaneousDataViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly SupplierStore _supplierStore;
        private readonly LocationStore _locationStore;
        public ObservableCollection<string> SupplierNames => _supplierStore.SupplierNames;
        public ObservableCollection<string> LocationIds => _locationStore.LocationIds;
        IDatabaseSynchronizationService _databaseSynchronizationService;

        public string SupplierName { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string SupplierNameFilter { get; set; } = "";


        public string LocationId { get; set; } = "";
        public string Note { get; set; } = "";
        public string LocationIdFilter { get; set; } = "";
        //
        private List<SupplierDto> suppliers = new();
        public ObservableCollection<SupplierEntryViewModel> SupplierEntries { get; set; } = new();
        public ICommand LoadAllSuppliersCommand { get; set; }
        public ICommand FilterSuppliersCommand { get; set; }
        public ICommand CreateSupplierCommand { get; set; }
        public ICommand LoadBothViewCommand { get; set; }
        //
        private List<LocationDto> locations = new();
        public ObservableCollection<LocationEntryViewModel> LocationEntries { get; set; } = new();
        public ICommand LoadAllLocationsCommand { get; set; }
        public ICommand FilterLocationsCommand { get; set; }
        public ICommand CreateLocationCommand { get; set; }
        public MiscellaneousDataViewModel(IApiService apiService, IMapper mapper, SupplierStore supplierStore, LocationStore locationStore, IDatabaseSynchronizationService databaseSynchronizationService)
        {
            _apiService = apiService;
            _mapper = mapper;
            _supplierStore = supplierStore;
            _locationStore = locationStore;
            _databaseSynchronizationService = databaseSynchronizationService;

            LoadAllSuppliersCommand = new RelayCommand(LoadAllSuppliersAsync);
            FilterSuppliersCommand = new RelayCommand(FilterSupplier);
            CreateSupplierCommand = new RelayCommand(CreateSupplierAsync);

            LoadAllLocationsCommand = new RelayCommand(LoadAllLocationsAsync);
            FilterLocationsCommand = new RelayCommand(FilterLocation);
            CreateLocationCommand = new RelayCommand(CreateLocationAsync);

            LoadBothViewCommand = new RelayCommand(LoadBothView);
        }

        private async void LoadAllSuppliersAsync()
        {
            suppliers = (await _apiService.GetAllSuppliersAsync()).ToList();
            var filteredSupplierDtos = suppliers.Where(i => i.SupplierName.Contains(SupplierNameFilter));
            var filteredSuppliers = _mapper.Map<IEnumerable<SupplierDto>, IEnumerable<SupplierEntryViewModel>>(filteredSupplierDtos);

            SupplierEntries = new ObservableCollection<SupplierEntryViewModel>(filteredSuppliers);
            foreach (var entry in SupplierEntries)
            {
                entry.SetApiService(_apiService);
                entry.SetMapper(_mapper);
                entry.Updated += LoadAllSuppliersAsync;
                entry.OnException += Error;
            }
        }

        private void FilterSupplier()
        {
            var filteredSupplierDtos = suppliers.Where(i => i.SupplierName.Contains(SupplierNameFilter));
            var filteredSuppliers = _mapper.Map<IEnumerable<SupplierDto>, IEnumerable<SupplierEntryViewModel>>(filteredSupplierDtos);

            SupplierEntries = new ObservableCollection<SupplierEntryViewModel>(filteredSuppliers);
            foreach (var entry in SupplierEntries)
            {
                entry.SetApiService(_apiService);
                entry.SetMapper(_mapper);
                entry.Updated += LoadAllSuppliersAsync;
                entry.OnException += Error;
            }
        }
        private void LoadManageSupplierView()
        {
            _databaseSynchronizationService.SynchronizeSuppliersData();
            LoadAllSuppliersAsync();
            OnPropertyChanged(nameof(SupplierNames));
        }

        private async void CreateSupplierAsync()
        {
            var createSupplierDto = new SupplierDto(
                SupplierName,
                Address,
                PhoneNumber);
            try
            {
                await _apiService.CreateSupplier(createSupplierDto);
                LoadManageSupplierView();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mã vật tư đã tồn tại.");
            }
            catch (Exception)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Không thể tạo vật tư mới.");
            }
            MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            SupplierName = "";
            Address = "";
            PhoneNumber = "";
            LoadManageSupplierView();
        }

        private async void LoadAllLocationsAsync()
        {
            locations = (await _apiService.GetAllLocationsAsync()).ToList();
            var filteredLocationDtos = locations.Where(i => i.LocationId.Contains(LocationIdFilter));
            var filteredLocations = _mapper.Map<IEnumerable<LocationDto>, IEnumerable<LocationEntryViewModel>>(filteredLocationDtos);

            LocationEntries = new ObservableCollection<LocationEntryViewModel>(filteredLocations);
            foreach (var entry in LocationEntries)
            {
                entry.SetApiService(_apiService);
                entry.SetMapper(_mapper);
                entry.Updated += LoadAllLocationsAsync;
                entry.OnException += Error;
            }
        }

        private void FilterLocation()
        {
            var filteredLocationDtos = locations.Where(i => i.LocationId.Contains(LocationIdFilter));
            var filteredLocations = _mapper.Map<IEnumerable<LocationDto>, IEnumerable<LocationEntryViewModel>>(filteredLocationDtos);

            LocationEntries = new ObservableCollection<LocationEntryViewModel>(filteredLocations);
            foreach (var entry in LocationEntries)
            {
                entry.SetApiService(_apiService);
                entry.SetMapper(_mapper);
                entry.Updated += LoadAllLocationsAsync;
                entry.OnException += Error;
            }
        }
        private void LoadManageLocationView()
        {
            _databaseSynchronizationService.SynchronizeLocationsData();
            LoadAllLocationsAsync();
            OnPropertyChanged(nameof(LocationIds));
        }

        private async void CreateLocationAsync()
        {
            var createLocationDto = new LocationDto(LocationId, Note);
            try
            {
                await _apiService.CreateLocation(createLocationDto);
                LoadManageLocationView();
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
            catch (DuplicateEntityException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mã vật tư đã tồn tại.");
            }
            catch (Exception)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Không thể tạo vật tư mới.");
            }
            MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LocationId = "";
            Note = "";
            LoadManageLocationView();
        }

        private void LoadBothView()
        {
            LoadManageSupplierView();
            LoadManageLocationView();
        }

        private void Error()
        {
            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        }
    }
}
