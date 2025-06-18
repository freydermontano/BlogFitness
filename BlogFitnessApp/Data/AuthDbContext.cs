
// Importa clases necesarias para manejar la autenticacion con Identity
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogFitnessApp.Data
{

    // Esta clase hereda de IdentityDbContext, lo que significa que incluye toda la infraestructura
    // necesaria para manejar usuarios, roles, claims, tokens, etc.
    public class AuthDbContext : IdentityDbContext
    {
        private readonly IdentityDbContext identityDbContext;

        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }


        //Metodo que se ejecuta cuando EF Core está construyendo el modelo de la base de datos.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Se crea IDs fijos para cada rol

            var userRoleId = "c9c7f9f2-3b8a-4d5e-8b1a-6f1e2d3c4b5a";
            var adminRoleId = "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6";
            var superAdminRoleId = "f1e2d3c4-b5a6-7d8e-9f0g-h1i2j3k4l5m6";

            // Ver Roles Administrador, SuperAdmin, Usuario
            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name = "User", NormalizedName = "USER", Id = userRoleId, ConcurrencyStamp = userRoleId },
                new IdentityRole {Name = "Admin", NormalizedName = "ADMIN", Id = adminRoleId, ConcurrencyStamp = adminRoleId},
                new IdentityRole {Name = "SuperAdmin", NormalizedName ="SUPERADMIN", Id = superAdminRoleId, ConcurrencyStamp = superAdminRoleId}

            };


            //Configurar los roles en la base de datos
            builder.Entity<IdentityRole>().HasData(roles);

            //Super Admin por defecto
            var superAdminId = "12345678-1234-1234-1234-123456789012";

            var superAdmin = new IdentityUser
            {
                UserName = "superAdmin@gmail.com",
                Email = "superAdmin@gmail.com",
                NormalizedEmail = "superAdmin@gmail.com".ToLower(),
                NormalizedUserName = "superAdmin@gmail.com".ToLower(),
                Id = superAdminId,

            };

            superAdmin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdmin, "superAdmin1234");
            builder.Entity<IdentityUser>().HasData(superAdmin);

            //Agregar todos los roles al SuperAdmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = superAdminId, RoleId = userRoleId },
                new IdentityUserRole<string> { UserId = superAdminId, RoleId = adminRoleId },
                new IdentityUserRole<string> { UserId = superAdminId, RoleId = superAdminRoleId }
            };


            // Configurar los roles del SuperAdmin en la base de datos
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }

    }
}
