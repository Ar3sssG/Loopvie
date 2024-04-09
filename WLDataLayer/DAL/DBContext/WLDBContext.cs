using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WLDataLayer.DAL.Entities;
using WLDataLayer.Identity;

namespace WLDataLayer.DAL.DBContext
{
    public class WLDBContext : IdentityDbContext<User,Role,int>
    {
        public WLDBContext(DbContextOptions<WLDBContext> options)
            : base(options)
        {
        }

        #region DbSets

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    ///TODO******************************************************************************************************************************************
        //    var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
        //    entities.ForEach(e =>
        //    {
        //        e.Property("CreationDate").CurrentValue = DateTime.UtcNow;

        //        e.Property("CreatedById").CurrentValue = 11;
        //    });
            
        //    entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
        //    entities.ForEach(e =>
        //    {
        //        e.Property("UpdateDate").CurrentValue = DateTime.UtcNow;
        //        e.Property("LastModifiedById").CurrentValue = 11;
        //    });

        //    return await SaveChangesAsync(cancellationToken);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                 .ToTable("Users");

            modelBuilder.Entity<Role>()
                 .ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<int>>()
                 .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim<int>>()
                 .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserLogin<int>>()
                .ToTable("UserLogins");

            modelBuilder.Entity<IdentityRoleClaim<int>>()
                .ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserToken<int>>()
                .ToTable("UsersTokens");

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1 ,Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = 2 ,Name = "User", NormalizedName = "USER" });

        }
    }
}
