using DataCheckingTool.Application;
using DataCheckingTool.Application.Contracts;
using DataCheckingTool.EntityFrameworkCore;
using DataCheckingToolApi.DataCheckingTool;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nm.ExcelHelper;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DataCheckingToolApi
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(DCTEntityFrameworkCoreModule))]
    [DependsOn(typeof(DCTApplicationModule))]
    [DependsOn(typeof(DCTApplicationContractsModule))]
    [DependsOn(typeof(NmExcelHelperModule))]
    public class AppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var builder = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource
                {
                    Path = "DataCheckingToolDataOptions.json",
                    Optional = false,
                    ReloadOnChange = true
                }).Build();
            context.Services.Configure<DataCheckingToolDataOptions>(builder);
            context.Services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    if (string.IsNullOrEmpty(""))
                    {
                        builder.SetIsOriginAllowed(_ => true) //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();//指定处理cookie
                    }
                    else
                    {
                        builder.WithOrigins("")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();//指定处理cookie
                    }
                });
            });
        }
        public override void OnApplicationInitialization(
            ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("any");
            app.UseConfiguredEndpoints();
        }
    }
}
