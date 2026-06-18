var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC al contenedor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración básica del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta convencional MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
