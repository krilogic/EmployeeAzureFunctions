using Employee.Azure;
using Employee.Azure.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Employee.Azure
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddDbContext<EmployeeRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
