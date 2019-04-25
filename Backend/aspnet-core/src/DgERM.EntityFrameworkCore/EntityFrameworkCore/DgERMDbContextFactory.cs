using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DgERM.Configuration;
using DgERM.Web;

namespace DgERM.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DgERMDbContextFactory : IDesignTimeDbContextFactory<DgERMDbContext>
    {
        public DgERMDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DgERMDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DgERMDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DgERMConsts.ConnectionStringName));

            return new DgERMDbContext(builder.Options);
        }
    }
}
