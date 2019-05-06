using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Dg.ERM.Authorization.Roles;
using Dg.ERM.Authorization.Users;
using Dg.ERM.MultiTenancy;
using DG.ERM.Products;
using Dg.ERM.Authorization.ExtendInfos;

namespace Dg.ERM.EntityFrameworkCore
{
    public class ERMDbContext : AbpZeroDbContext<Tenant, Role, User, ERMDbContext>
    {
        /* Define a DbSet for each entity of the application */ 
        public DbSet<Product> Products { get; set; }

        public DbSet<ExtendInfo> ExtendInfos { get; set; }

        public ERMDbContext(DbContextOptions<ERMDbContext> options)
            : base(options)
        {
        }
    }
}
