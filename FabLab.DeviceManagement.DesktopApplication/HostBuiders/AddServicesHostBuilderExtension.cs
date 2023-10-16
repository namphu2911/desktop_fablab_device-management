using FabLab.DeviceManagement.DesktopApplication.Core.Application.Mapping;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.Services;
using FabLab.DeviceManagement.DesktopApplication.Core.Application.Store;
using FabLab.DeviceManagement.DesktopApplication.Core.Domain.Services;
using FabLab.DeviceManagement.DesktopApplication.Core.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabLab.DeviceManagement.DesktopApplication.HostBuiders
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                object value = services.AddAutoMapper(typeof(ApplicationDbContext));

                services.AddSingleton<EquipmentStore>();
                services.AddSingleton<EquipmentTypeStore>();
                services.AddSingleton<LocationStore>();
                services.AddSingleton<SupplierStore>();

                services.AddSingleton<IApiService, ApiService>();
                services.AddSingleton<IDatabaseSynchronizationService, DatabaseSynchronizationService>();

            });
            return host;
        }
    }
}
