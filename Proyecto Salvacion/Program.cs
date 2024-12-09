using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Salvacion.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext con Identity
builder.Services.AddDbContext<SalonComunalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar ASP.NET Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SalonComunalContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Crear roles y usuarios por defecto al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Crear roles por defecto si no existen
        string[] roleNames = { "Administrador", "Comprador" };
        foreach (var roleName in roleNames)
        {
            var roleExists = roleManager.RoleExistsAsync(roleName).Result;
            if (!roleExists)
            {
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
            }
        }

        // Crear un usuario Administrador por defecto si es necesario
        var defaultAdmin = userManager.FindByEmailAsync("admin@proyecto.com").Result;
        if (defaultAdmin == null)
        {
            var user = new IdentityUser
            {
                UserName = "admin@proyecto.com",
                Email = "admin@proyecto.com",
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, "AdminPassword123!").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Administrador").Wait();
            }
        }
    }
    catch (Exception ex)
    {
        // Manejar cualquier error durante la creación de roles o usuarios
        Console.WriteLine($"Error al crear roles o usuarios: {ex.Message}");
    }
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
