using DataCheckingTool.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace DataCheckingTool.Application
{
    [DependsOn(typeof(DCTApplicationContractsModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class DCTApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DCTApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DataCheckingToolMapperProfile>(validate: true);
            });
        }
    }
}
