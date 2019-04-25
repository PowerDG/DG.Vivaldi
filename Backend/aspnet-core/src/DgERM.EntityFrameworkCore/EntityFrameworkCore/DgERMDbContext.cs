using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using DgERM.Authorization.Roles;
using DgERM.Authorization.Users;
using DgERM.MultiTenancy;
using DgERM.DgCore.Tasks;

namespace DgERM.EntityFrameworkCore
{
    public class DgERMDbContext : AbpZeroDbContext<Tenant, Role, User, DgERMDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Task> Taskss { get; set; }
        public DgERMDbContext(DbContextOptions<DgERMDbContext> options)
            : base(options)
        {
        }
    }
}
