using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DataCheckingTool.EntityFrameworkCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    public class DCTEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DCToolBaseDbcontext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
            Configure<AbpDbContextOptions>(options =>
            {
                //options.UseOracle(Configuration.GetConnectionString("DefaultConnection"))
                /* The main point to change your DBMS.
                 * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                options.Configure(ctx =>
                {
                    if (ctx.ExistingConnection != null)
                    {
                        ctx.DbContextOptions.UseOracle(ctx.ExistingConnection);
                    }
                    else
                    {
                        ctx.DbContextOptions.UseOracle(ctx.ConnectionString);
                    }
                });
            });
        }
    }
}
