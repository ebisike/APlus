using APlus.Application.Implementation;
using APlus.Application.Interface;
using APlus.DataAccess.UnitOfWork.Implementation;
using APlus.DataAccess.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.IoC.Dependencies
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IInventoryItemService, InventoryItemService>();
        }
    }
}
