﻿using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.Store;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
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
    public class DeviceEntryViewModel : BaseViewModel
    {
        private SupplierStore? _supplierStore; 
        private LocationStore? _locationStore;
        private EquipmentTypeStore? _equipmentTypeStore;
        public ObservableCollection<string>? SupplierNames => _supplierStore?.SupplierNames;
        public ObservableCollection<string>? LocationIds => _locationStore?.LocationIds;
        public ObservableCollection<string>? EquipmentTypeIds => _equipmentTypeStore?.EquipmentTypeIds;
        public ObservableCollection<string>? EquipmentTypeNames => _equipmentTypeStore?.EquipmentTypeNames;
        private IApiService? _apiService;
        private IMapper? _mapper;
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public DateTime YearOfSupply { get; set; }
        public string CodeOfManager { get; set; }
        public string LocationId { get; set; }
        public string SupplierName { get; set; }
        public EStatus Status { get; set; }

        private string? equipmentTypeId;
        private string? equipmentTypeName;
        public string? EquipmentTypeId
        {
            get
            {
                return equipmentTypeId;
            }
            set
            {
                equipmentTypeId = value;
                if (_equipmentTypeStore is not null && !String.IsNullOrEmpty(value))
                {
                    var equipmentType = _equipmentTypeStore.EquipmentTypes.First(i => i.EquipmentTypeId == equipmentTypeId);
                    equipmentTypeName = equipmentType.EquipmentTypeName;
                    OnPropertyChanged(nameof(EquipmentTypeName));
                }
            }

        }
        public string? EquipmentTypeName
        {
            get
            {
                return equipmentTypeName;
            }
            set
            {
                equipmentTypeName = value;
                if (_equipmentTypeStore is not null && !String.IsNullOrEmpty(value))
                {
                    var equipmentType = _equipmentTypeStore.EquipmentTypes.First(i => i.EquipmentTypeName == equipmentTypeName);
                    equipmentTypeId = equipmentType.EquipmentTypeId;
                    OnPropertyChanged(nameof(EquipmentTypeId));
                }
            }
        }
        public event Action? Updated;
        public event Action? OnException;
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DeviceEntryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            SaveCommand = new RelayCommand(SaveAsync);
            DeleteCommand = new RelayCommand(DeleteAsync);
        }
        public DeviceEntryViewModel(string equipmentId, string equipmentName, DateTime yearOfSupply, string codeOfManager, string locationId, string supplierName, EStatus status, string? equipmentTypeId, string? equipmentTypeName) : this()
        {
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
            YearOfSupply = yearOfSupply;
            CodeOfManager = codeOfManager;
            LocationId = locationId;
            SupplierName = supplierName;
            Status = status;
            EquipmentTypeId = equipmentTypeId;
            EquipmentTypeName = equipmentTypeName;
        }

        public void SetApiService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public void SetMapper(IMapper mapper)
        {
            _mapper = mapper;
            OnPropertyChanged();
        }
        public void SetStore(SupplierStore supplierStore, LocationStore locationStore, EquipmentTypeStore equipmentTypeStore)
        {
            _supplierStore = supplierStore;
            _locationStore = locationStore;
            _equipmentTypeStore = equipmentTypeStore;
            OnPropertyChanged();
        }

        private async void SaveAsync()
        {

            FixEquipmentDto fixDto = new FixEquipmentDto(EquipmentId, EquipmentName, YearOfSupply, CodeOfManager, Status, LocationId, SupplierName, EquipmentTypeId);
            if (_mapper is not null && _apiService is not null)
            {
                try
                {
                    await _apiService.FixEquipmentAsync(fixDto);
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (HttpRequestException)
                {
                    OnException?.Invoke();
                    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
                }
            }
            Updated?.Invoke();
        }

        private async void DeleteAsync()
        {
            if (_mapper is not null && _apiService is not null)
            {
                try
                {
                    if (MessageBox.Show("Xác nhận xóa", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        await _apiService.DeleteEquipmentAsync(EquipmentId);
                        Updated?.Invoke();
                        MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else { }
                }
                catch (HttpRequestException)
                {
                    OnException?.Invoke();
                    ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
                }
            }
            
        }
    }
}
