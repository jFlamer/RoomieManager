using RoomieManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./keys"))
    .SetApplicationName("RoomieManager");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RoomieManagerDBContext>(options =>
    options.UseSqlite("Data Source=RoomieManager.db"));
builder.Services.AddSession();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RoomieManagerDBContext>();
    dbContext.Database.EnsureCreated();
    DbInitializer.Initialize(dbContext);
}

app.UseSession();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        string ogPath = context.Request.Path.Value;
        context.Items["originalPath"] = ogPath;
        context.Request.Path = "/IO/Login";
        await next();
    }
});

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();


app.Use(async (ctx, next) =>
{
    await next();

    if(ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/IO/Logowanie";
        await next();
    }
});


app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=IO}/{action=Login}/{id?}");

app.Run();
