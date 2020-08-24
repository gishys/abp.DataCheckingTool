using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DataCheckingTool.EntityFrameworkCore
{
    [ConnectionStringName("DCToolDatabase")]
    public class DCToolBaseDbcontext : AbpDbContext<DCToolBaseDbcontext>
    {
        public DCToolBaseDbcontext(
            DbContextOptions<DCToolBaseDbcontext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
