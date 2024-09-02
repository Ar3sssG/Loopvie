using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoopvieDataLayer.DAL.Entities;
using LoopvieDataLayer.Identity;

namespace LoopvieDataLayer.DAL.DBContext
{
    public class LoopvieDBContext : IdentityDbContext<User, Role, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoopvieDBContext(DbContextOptions<LoopvieDBContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region DbSets

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<UserAchievement> UserAchievements { get; set; }
        #endregion


        #region SaveChangesAsync

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value == null ? 0 : Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirst("UserId").Value);
                var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;
                        if (userId != default(int))
                        {
                            ((BaseEntity)entityEntry.Entity).UpdatedById = userId;
                        }
                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                        if (userId != default(int))
                        {
                            ((BaseEntity)entityEntry.Entity).CreatedById = userId;
                        }
                    }
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

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
            new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = 2, Name = "User", NormalizedName = "USER" });
        }
    }
}
