using Dapper;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace DataCheckingTool.EntityFrameworkCore
{
    public class BaseDapperRepository<T> : DapperRepository<T>, ITransientDependency where T : AbpDbContext<T>
    {
        public BaseDapperRepository(
            IDbContextProvider<T> dbContextProvider)
        : base(dbContextProvider)
        {

        }
        public List<T1> Query<T1>(string sql, int page = 0, int pageSize = 0)
        {
            var entity = DbConnection
                .QueryAsync<T1>(sql, transaction: DbTransaction);
            if (pageSize == 0)
                return entity.Result.AsQueryable().ToList();
            return entity.Result.AsQueryable().PageBy(page, pageSize).ToList();
        }
        public int Execute(string sql)
        {
            return DbConnection
                .Execute(sql, transaction: DbTransaction);
        }
        public int QueryCount(string sql)
        {
            var entity = DbConnection
                .QueryAsync(sql, transaction: DbTransaction).Result.AsQueryable();
            return entity.Count();
        }
    }
}
