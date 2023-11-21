using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.Store;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.ViewModels.SeedWork;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.EquipmentTypes;
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
    public class EquipmentTypeViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly EquipmentTypeStore _equipmentTypeStore;
        public string EquipmentTypeId { get; set; } = "";
        public string EquipmentTypeName { get; set; } = "";
        public ECategory Category { get; set; }

        //Create New Equipment
        public string NewEquipmentTypeId { get; set; } = "";
        public string NewEquipmentTypeName { get; set; } = "";
        public ECategory NewCategory { get; set; }
        public string NewPicture { get; set; } = "";
        private List<EquipmentTypeDto> equipmentTypes = new();
        private List<EquipmentTypeDto> filteredEquipmentTypes = new();
        public ObservableCollection<EquipmentTypeEntryViewModel> EquipmentTypeEntries { get; set; } = new();
        public ObservableCollection<string> EquipmentTypeIds => _equipmentTypeStore.EquipmentTypeIds;
        public ObservableCollection<string> EquipmentTypeNames => _equipmentTypeStore.EquipmentTypeNames;
        public ICommand LoadEquipmentTypeEntriesCommand { get; set; }
        public ICommand LoadInitialCommand { get; set; }
        public ICommand LoadEquipmentTypeViewCommand { get; set; }
        public ICommand CreateEquipmentTypeCommand { get; set; }

        public EquipmentTypeViewModel(IApiService apiService, IMapper mapper, EquipmentTypeStore equipmentTypeStore)
        { 
            _apiService = apiService;
            _mapper = mapper;
            _equipmentTypeStore = equipmentTypeStore;

            LoadInitialCommand = new RelayCommand(LoadInitial);
            LoadEquipmentTypeEntriesCommand = new RelayCommand(LoadEquipmentTypeEntries);
            LoadEquipmentTypeViewCommand = new RelayCommand(LoadEquipmentTypeView);
            CreateEquipmentTypeCommand = new RelayCommand(CreateEquipmentTypeAsync);
        }

        private void LoadEquipmentTypeView()
        {
            LoadInitial();
            OnPropertyChanged(nameof(EquipmentTypeIds));
            OnPropertyChanged(nameof(EquipmentTypeNames));
        }

        private async void LoadInitial()
        {
            Category = ECategory.All;

            EquipmentTypeId = "";
            EquipmentTypeName = "";

            try
            {
                equipmentTypes = (await _apiService.GetAllEquipmentTypesAsync()).ToList();
                //var filteredEquipmentsDtos = equipments.Where(i => i.ItemId.Contains(ItemIdFilter));

                var viewModels = _mapper.Map<IEnumerable<EquipmentTypeDto>, IEnumerable<EquipmentTypeEntryViewModel>>(equipmentTypes);
                EquipmentTypeEntries = new(viewModels);

                foreach (var entry in EquipmentTypeEntries)
                {
                    entry.SetApiService(_apiService);
                    entry.SetMapper(_mapper);
                    entry.Updated += LoadInitial;
                    entry.OnException += Error;
                }
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Đã có lỗi xảy ra: Mất kết nối với server.");
            }
        }

        private void LoadEquipmentTypeEntries()
        {
            try
            {
                if (Category == ECategory.All)
                {
                    filteredEquipmentTypes = equipmentTypes;
                    if (!String.IsNullOrEmpty(EquipmentTypeId))
                    {
                        filteredEquipmentTypes = equipmentTypes.Where(i => i.EquipmentTypeId.Contains(EquipmentTypeId)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentTypeName))
                    {
                        filteredEquipmentTypes = equipmentTypes.Where(i => i.EquipmentTypeName.Contains(EquipmentTypeName)).ToList();
                    }
                    
                    var viewModels = _mapper.Map<IEnumerable<EquipmentTypeDto>, IEnumerable<EquipmentTypeEntryViewModel>>(filteredEquipmentTypes);
                    EquipmentTypeEntries = new(viewModels);
                }
                else
                {
                    filteredEquipmentTypes = equipmentTypes.Where(i => i.Category == Category).ToList();
                    if (!String.IsNullOrEmpty(EquipmentTypeId))
                    {
                        filteredEquipmentTypes = equipmentTypes.Where(i => i.EquipmentTypeId.Contains(EquipmentTypeId)).ToList();
                    }
                    if (!String.IsNullOrEmpty(EquipmentTypeName))
                    {
                        filteredEquipmentTypes = equipmentTypes.Where(i => i.EquipmentTypeName.Contains(EquipmentTypeName)).ToList();
                    }

                    var viewModels = _mapper.Map<IEnumerable<EquipmentTypeDto>, IEnumerable<EquipmentTypeEntryViewModel>>(filteredEquipmentTypes);
                    EquipmentTypeEntries = new(viewModels);
                }

                foreach (var entry in EquipmentTypeEntries)
                {
                    entry.SetApiService(_apiService);
                    entry.SetMapper(_mapper);
                    entry.Updated += LoadEquipmentTypeEntries;
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

        private async void CreateEquipmentTypeAsync()
        {
            if(NewCategory == ECategory.All)
            {
                MessageBox.Show("Lĩnh vực sai, mời chọn lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var createDto = new EquipmentTypeDto(NewEquipmentTypeId, NewPicture, NewEquipmentTypeName, NewCategory);
                try
                {
                    await _apiService.CreateEquipmentType(createDto);
                    MessageBox.Show("Đã Cập Nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadEquipmentTypeEntries();
                    LoadEquipmentTypeView();
                    NewEquipmentTypeId = "";
                    NewEquipmentTypeName = "";
                    NewCategory = ECategory.Mechanical;
                    NewPicture = "";
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

            }

            //LoadManageItemView();
        }

    }
}
