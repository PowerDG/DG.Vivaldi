using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Dg.ERM.Configuration;
using Dg.ERM.Web;

namespace Dg.ERM.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ERMDbContextFactory : IDesignTimeDbContextFactory<ERMDbContext>
    {
        public ERMDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ERMDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ERMDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ERMConsts.ConnectionStringName));

            return new ERMDbContext(builder.Options);
        }
    }
}
