using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.EquipmentTypes;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Exceptions;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Models.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private const string serverUrl = "https://fablab20231120154444old.azurewebsites.net";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<EquipmentDto>> GetAllEquipmentsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Equipment/Search1");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var equipments = JsonConvert.DeserializeObject<IEnumerable<EquipmentDto>>(responseBody);
            if (equipments is null)
            {
                return new List<EquipmentDto>();
            }
            return equipments;
        }

        public async Task<IEnumerable<EquipmentDto>> GetEquipmentsRecordsAsync(string yearSelected, string equipmentId, string equipmentTypeId, ECategory? category)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Equipment/Search1?equipmentId={equipmentId}&YearOfSupply={yearSelected}&equipmentTypeId={equipmentTypeId}&Category={category}");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var equipments = JsonConvert.DeserializeObject<IEnumerable<EquipmentDto>>(responseBody);
            if (equipments is null)
            {
                return new List<EquipmentDto>();
            }
            return equipments;
        }

        public async Task CreateEquipment(CreateEquipmentDto equipment)
        {
            var json = JsonConvert.SerializeObject(equipment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Equipment", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ServerSideError>(responseBody);
                    if (error is not null)
                    {
                        switch (error.Code)
                        {
                            case "Domain.EntityDuplication":
                                throw new DuplicateEntityException();
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        public async Task FixEquipmentAsync(FixEquipmentDto fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{serverUrl}/api/Equipment", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEquipmentAsync(string equipmentId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/Equipment?name={equipmentId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Location");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var responses = JsonConvert.DeserializeObject<IEnumerable<LocationDto>>(responseBody);
            if (responses is null)
            {
                return new List<LocationDto>();
            }
            return responses;
        }

        public async Task CreateLocation(LocationDto location)
        {
            var json = JsonConvert.SerializeObject(location);
            var jsonCamelCase = JsonNamingPolicy.CamelCase.ConvertName(json);

            var content = new StringContent(jsonCamelCase, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Location", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ServerSideError>(responseBody);
                    if (error is not null)
                    {
                        switch (error.Code)
                        {
                            case "Domain.EntityDuplication":
                                throw new DuplicateEntityException();
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        public async Task FixLocationAsync(LocationDto fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{serverUrl}/api/Location", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLocationAsync(string locationId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/Location?name={locationId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Supplier");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var responses = JsonConvert.DeserializeObject<IEnumerable<SupplierDto>>(responseBody);
            if (responses is null)
            {
                return new List<SupplierDto>();
            }
            return responses;
        }

        public async Task CreateSupplier(SupplierDto supplier)
        {
            var json = JsonConvert.SerializeObject(supplier);
            var jsonCamelCase = JsonNamingPolicy.CamelCase.ConvertName(json);

            var content = new StringContent(jsonCamelCase, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Supplier", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ServerSideError>(responseBody);
                    if (error is not null)
                    {
                        switch (error.Code)
                        {
                            case "Domain.EntityDuplication":
                                throw new DuplicateEntityException();
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        public async Task FixSupplierAsync(SupplierDto fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{serverUrl}/api/Supplier", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSupplierAsync(string supplierName)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/Supplier?name={supplierName}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<EquipmentTypeDto>> GetAllEquipmentTypesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/EquipmentType");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var responses = JsonConvert.DeserializeObject<IEnumerable<EquipmentTypeDto>>(responseBody);
            if (responses is null)
            {
                return new List<EquipmentTypeDto>();
            }
            return responses;
        }

        public async Task CreateEquipmentType(EquipmentTypeDto equipmentType)
        {
            var json = JsonConvert.SerializeObject(equipmentType);
            var jsonCamelCase = JsonNamingPolicy.CamelCase.ConvertName(json);

            var content = new StringContent(jsonCamelCase, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/EquipmentType", content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ServerSideError>(responseBody);
                    if (error is not null)
                    {
                        switch (error.Code)
                        {
                            case "Domain.EntityDuplication":
                                throw new DuplicateEntityException();
                            default : throw ex;
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        public async Task FixEquipmentTypesAsync(EquipmentTypeDto fixDto)
        {
            var json = JsonConvert.SerializeObject(fixDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{serverUrl}/api/EquipmentType", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEquipmentTypeAsync(string equipmentTypeId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{serverUrl}/api/EquipmentType?name=string{equipmentTypeId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
