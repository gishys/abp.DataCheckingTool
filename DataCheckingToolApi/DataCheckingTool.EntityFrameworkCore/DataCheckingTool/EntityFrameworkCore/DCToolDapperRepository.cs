using Volo.Abp.EntityFrameworkCore;

namespace DataCheckingTool.EntityFrameworkCore
{
    public class DCToolDapperRepository : BaseDapperRepository<DCToolBaseDbcontext>
    {
        public DCToolDapperRepository(
            IDbContextProvider<DCToolBaseDbcontext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
    }
}
