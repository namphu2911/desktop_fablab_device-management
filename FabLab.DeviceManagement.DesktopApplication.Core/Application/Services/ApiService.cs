using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Equipments;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Locations;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Dtos.Suppliers;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Exceptions;
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

namespace FabLab.DeviceManagement.DesktopApplication.Core.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private const string serverUrl = "https://thaiduongwarehousedemo.azurewebsites.net/";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<EquipmentDto>> GetAllEquipmentsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Items");

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
            //var jsonCamelCase = JsonNamingPolicy.CamelCase.ConvertName(json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            

            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Items/item", content);

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

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Items");

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


            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Items/item", content);

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

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Items");

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


            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Items/item", content);

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

        public async Task<IEnumerable<EquipmentTypeDto>> GetAllEquipmentTypesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{serverUrl}/api/Items");

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


            HttpResponseMessage response = await _httpClient.PostAsync($"{serverUrl}/api/Items/item", content);

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
    }
}
