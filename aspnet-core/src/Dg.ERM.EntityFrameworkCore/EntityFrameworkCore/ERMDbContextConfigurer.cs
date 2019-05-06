using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Dg.ERM.EntityFrameworkCore
{
    public static class ERMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ERMDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString); 
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ERMDbContext> builder, DbConnection connection)
        {
            //    builder.UseSqlServer(connection); 
            builder.UseNpgsql(connection);
        }
    }
}
