using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;
using UsuariosApi.Modelo;

namespace UsuariosApi.Data;

public class UsuarioContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>  

{
    private IConfiguration _configuration;
    public UsuarioContext(DbContextOptions<UsuarioContext> opt, IConfiguration configuration) : base(opt)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        CustomIdentityUser admin = new CustomIdentityUser
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            CEP = "02468040",
            Logradouro = "RUA IBIRATINGA",
            Numero = 104,
            Bairro = "IMIRIM",
            Complemento = "CASA",
            Localidade = "SÃO PAULO",
            UF = "SP",
            CPF = "87817936005",
            Status = true,
            Perfil = "ADMIN",
            Nome = "ADMINISTRADOR",
            Id = 99999
        };  


        PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

        admin.PasswordHash = hasher.HashPassword(admin,
                   _configuration.GetValue<string>("admininfo:password"));

        builder.Entity<CustomIdentityUser>().HasData(admin);

        builder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN"}
            );
        builder.Entity<IdentityRole<int>>().HasData(
          new IdentityRole<int> { Id = 99997, Name = "lojista", NormalizedName = "LOJISTA" }
          );
        builder.Entity<IdentityRole<int>>().HasData(
          new IdentityRole<int> { Id = 99995, Name = "cliente", NormalizedName = "CLIENTE" }
          );

        builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 });



    }
}
