using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DgERM.EntityFrameworkCore
{
    public static class DgERMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DgERMDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            //builder.UseSqlServer(connectionString);
            builder.UseNpgsql(connectionString);
            //options.UseNpgsql(sqlConnectionString)
        }

        public static void Configure(DbContextOptionsBuilder<DgERMDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);

            builder.UseNpgsql(connection);
        }
    }
}
