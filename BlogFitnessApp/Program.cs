using BlogFitnessApp.Data;
using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Agregar Servicios
// Configurar DbContext
builder.Services.AddDbContext<BLogFitnessDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitnesBlogDbConnectionString")));

// Configurar Identity para autenticación y autorización
builder.Services.AddDbContext<AuthDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitnesAuthBlogDbConnectionString")));

// Configurar Identity con opciones personalizadas
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();


// Registro del repositorio de Tags para inyeccion de dependencias,
// permitiendo usar ITagRepository con su implementación TagRepositoryImpl
builder.Services.AddScoped<ITagRepository, TagRepositoryImpl>();
// Registro del repositorio de BlogPosts para inyeccion de dependencias,
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepositoryImpl>();

// Registro del repositorio de imágenes para inyección de dependencias
builder.Services.AddScoped<IImageRepository, ClaudinaryImageRepositoryImpl>();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configurar autenticacion antes de autorizacion cuando trabajemos con autenticacion
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
