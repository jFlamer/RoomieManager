using Microsoft.EntityFrameworkCore;
using RoomieManager.Data;
using RoomieManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RoomieManagerDBContext>(options =>
    options.UseSqlite("Data Source=RoomieManager.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RoomieManagerDBContext>();
    
    var user = new UserModel{ userName = "admin", password = "pass", isAdmin = true };
    db.Users.Add(user);
    db.SaveChanges();

    Console.WriteLine("Dodano użytkownika!");
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
