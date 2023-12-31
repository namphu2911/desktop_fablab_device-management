﻿using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.Store;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Exceptions;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
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
    public class DeviceManagementViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly EquipmentStore _equipmentStore;
        private readonly EquipmentTypeStore _equipmentTypeStore; 
        private readonly SupplierStore _supplierStore;
        private readonly LocationStore _locationStore;
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string YearSelected { get; set; } = "";

        private string equipmentId = "";
        private string equipmentName = "";
        private string equipmentTypeId = "";
        private string equipmentTypeName = "";

        public string EquipmentId { get; set; } = "";
        public string EquipmentName { get; set; } = "";
        public string EquipmentTypeId { get; set; } = "";
        public string EquipmentTypeName { get; set; } = "";

        //public string EquipmentId
        //{
        //    get
        //    {
        //        return equipmentId;
        //    }
        //    set
        //    {
        //        equipmentId = value;
        //        if (String.IsNullOrEmpty(value))
        //        {
        //            equipmentName = "";
        //            OnPropertyChanged(nameof(EquipmentName));
        //        }
        //        else
        //        {
        //            var equipment = _equipmentStore.Equipments.First(i => i.EquipmentId == equipmentId);
        //            equipmentName = equipment.EquipmentName;
        //            OnPropertyChanged(nameof(EquipmentName));
        //        }
        //    }

        //}
        //public string EquipmentName
        //{
        //    get
        //    {
        //        return equipmentName;
        //    }
        //    set
        //    {
        //        equipmentName = value;
        //        if (String.IsNullOrEmpty(value))
        //        {
        //            equipmentId = "";
        //            OnPropertyChanged(nameof(EquipmentId));
        //        }
        //        else
        //        {
        //            var equipment = _equipmentStore.Equipments.First(i => i.EquipmentName == equipmentName);
        //            equipmentId = equipment.EquipmentId;
        //            OnPropertyChanged(nameof(EquipmentId));
        //        }
        //    }
        //}

        //public string EquipmentTypeId
        //{
        //    get
        //    {
        //        return equipmentTypeId;
        //    }
        //    set
        //    {
        //        equipmentTypeId = value;
        //        if (String.IsNullOrEmpty(value))
        //        {
        //            equipmentTypeName = "";
        //            OnPropertyChanged(nameof(EquipmentTypeName));
        //        }
        //        else
        //        {
        //            var equipmentType = _equipmentTypeStore.EquipmentTypes.First(i => i.EquipmentTypeId == equipmentTypeId);
        //            equipmentTypeName = equipmentType.EquipmentTypeName;
        //            OnPropertyChanged(nameof(EquipmentTypeName));
        //        }
        //    }

        //}
        //public string EquipmentTypeName
        //{
        //    get
        //    {
        //        return equipmentTypeName;
        //    }
        //    set
        //    {
        //        equipmentTypeName = value;
        //        if (String.IsNullOrEmpty(value))
        //        {
        //            equipmentTypeId = "";
        //            OnPropertyChanged(nameof(EquipmentTypeId));
        //        }
        //        else
        //        {
        //            var equipmentType = _equipmentTypeStore.EquipmentTypes.First(i => i.EquipmentTypeName == equipmentTypeName);
        //            equipmentTypeId = equipmentType.EquipmentTypeId;
        //            OnPropertyChanged(nameof(EquipmentTypeId));
        //        }
        //    }
        //}

        //Create New Equipment
        public string NewEquipmentId { get; set; } = "";
        public string NewEquipmentName { get; set; } = "";
        public DateTime NewYearOfSupply { get; set; } = DateTime.Now.Date;
        public string NewCodeOfManage { get; set; } = "";
        public string NewSupplierName { get; set; } = "";
        public string NewLocationId { get; set; } = "";
        public string NewEquipmentTypeId { get; set; } = "";
        public EStatus NewStatus { get; set; }
        private List<EquipmentDto> equipments = new();
        private List<EquipmentDto> filteredEquipments = new();
        public ObservableCollection<DeviceEntryViewModel> DeviceEntries { get; set; } = new();
        public ECategory Category { get; set; }
        public ObservableCollection<string> EquipmentIds => _equipmentStore.EquipmentIds;
        public ObservableCollection<string> EquipmentNames => _equipmentStore.EquipmentNames;
        public ObservableCollection<string> EquipmentTypeIds => _equipmentTypeStore.EquipmentTypeIds;
        public ObservableCollection<string> EquipmentTypeNames => _equipmentTypeStore.EquipmentTypeNames;
        public ObservableCollection<string> SupplierNames => _supplierStore.SupplierNames;
        public ObservableCollection<string> LocationIds => _locationStore.LocationIds;
        public ObservableCollection<string> Years { get; set; } = new();    
        public ICommand LoadDeviceEntriesCommand { get; set; }
        public ICommand LoadInitialCommand { get; set; }
        public ICommand LoadDeviceManagementViewCommand { get; set; }
        public ICommand CreateEquipmentCommand { get; set; }

        public DeviceManagementViewModel(IApiService apiService, IMapper mapper, EquipmentStore equipmentStore, EquipmentTypeStore equipmentTypeStore, SupplierStore supplierStore, LocationStore locationStore)
        {
            _apiService = apiService;
            _mapper = mapper;
            _equipmentStore = equipmentStore;
            _equipmentTypeStore = equipmentTypeStore;
            _supplierStore = supplierStore;
            _locationStore = locationStore;
            Years = new ObservableCollection<string>();
            for (int i = DateTime.Now.Year; i >= 1975; i--)
            {
                Years.Add(i.ToString());
            }

            LoadInitialCommand = new RelayCommand(LoadInitial);
            LoadDeviceEntriesCommand = new RelayCommand(LoadDeviceEntries);
            LoadDeviceManagementViewCommand = new RelayCommand(LoadDeviceManagementView);
            CreateEquipmentCommand = new RelayCommand(CreateEquipmentAsync);
        }

        private void LoadDeviceManagementView()
        {
            LoadInitial();
            OnPropertyChanged(nameof(EquipmentIds));
            OnPropertyChanged(nameof(EquipmentNames));
            OnPropertyChanged(nameof(EquipmentTypeIds));
            OnPropertyChanged(nameof(EquipmentTypeNames));
            OnPropertyChanged(nameof(LocationIds));
            OnPropertyChanged(nameof(SupplierNames));

        }

        private async void LoadInitial()
        {
            Category = ECategory.All;

            YearSelected = "";
            EquipmentId = "";
            EquipmentName = "";
            EquipmentTypeId = "";
            EquipmentTypeName = "";

            try
            {
                equipments = (await _apiService.GetAllEquipmentsAsync()).ToList();
                //var filteredEquipmentsDtos = equipments.Where(i => i.ItemId.Contains(ItemIdFilter));
                
                var viewModels = _mapper.Map<IEnumerable<EquipmentDto>, IEnumerable<DeviceEntryViewModel>>(equipments);
                DeviceEntries = new(viewModels);
                
                foreach (var entry in DeviceEntries)
                {
                    entry.SetApiService(_apiService);
                    entry.SetMapper(_mapper);
                    entry.SetStore(_supplierStore, _locationStore, _equipmentTypeStore);
                    entry.Updated += LoadDeviceEntries;
                    entry.OnException += Error;
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private void LoadDeviceEntries()
        {
            try
            {
                if (Category == ECategory.All)
                {
                    filteredEquipments = equipments;
                    if (!String.IsNullOrEmpty(YearSelected))
                    {
                        filteredEquipments = equipments.Where(i => i.YearOfSupply.Year.ToString() == YearSelected).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentId))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentId.Contains(EquipmentId)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentName))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentName.Contains(EquipmentName)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentTypeId))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentType.EquipmentTypeId.Contains(EquipmentTypeId)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentTypeName))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentType.EquipmentTypeName.Contains(equipmentTypeName)).ToList();
                    }
                    //var filteredEquipmentsDtos = equipments.Where(i => i.ItemId.Contains(ItemIdFilter));
                    var viewModels = _mapper.Map<IEnumerable<EquipmentDto>, IEnumerable<DeviceEntryViewModel>>(filteredEquipments);
                    DeviceEntries = new(viewModels);
                }
                else
                {
                    filteredEquipments = equipments.Where(i => i.EquipmentType.Category == Category).ToList();
                    if (!String.IsNullOrEmpty(YearSelected))
                    {
                        filteredEquipments = equipments.Where(i => i.YearOfSupply.Year.ToString() == YearSelected).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentId))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentId.Contains(EquipmentId)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentName))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentName.Contains(EquipmentName)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentTypeId))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentType.EquipmentTypeId.Contains(EquipmentTypeId)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentTypeName))
                    {
                        filteredEquipments = equipments.Where(i => i.EquipmentType.EquipmentTypeName.Contains(equipmentTypeName)).ToList();
                    }
                    //var filteredEquipmentsDtos = equipments.Where(i => i.ItemId.Contains(ItemIdFilter));
                    var viewModels = _mapper.Map<IEnumerable<EquipmentDto>, IEnumerable<DeviceEntryViewModel>>(filteredEquipments);
                    DeviceEntries = new(viewModels);
                }

                foreach (var entry in DeviceEntries)
                {
                    entry.SetApiService(_apiService);
                    entry.SetMapper(_mapper);
                    entry.SetStore(_supplierStore, _locationStore, _equipmentTypeStore);
                    entry.Updated += LoadDeviceEntries;
                    entry.OnException += Error;
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private void Error()
        {
            ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
        }

        private async void CreateEquipmentAsync()
        {
            var createDto = new CreateEquipmentDto(
                NewEquipmentId,
                NewEquipmentName,
                NewYearOfSupply,
                NewCodeOfManage,
                NewStatus,
                NewLocationId, 
                NewSupplierName,
                NewEquipmentTypeId);
            try
            {
                await _apiService.CreateEquipment(createDto);
                LoadDeviceEntries();
                LoadDeviceManagementView();
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
            NewEquipmentId = "";
            NewEquipmentName = "";
            NewYearOfSupply = DateTime.Now.Date;
            NewCodeOfManage = "";
            NewLocationId = "";
            NewSupplierName = "";
            NewStatus = EStatus.Active;
            NewEquipmentTypeId = "";
            //LoadManageItemView();
        }

    }
}
