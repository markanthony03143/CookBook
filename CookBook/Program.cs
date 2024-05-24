using Microsoft.EntityFrameworkCore;
using CookBook.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CookBookDbContext>(
    opts => {
        opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:CookBookDbConnection"]);
    } ) ;

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options => {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Home/Logout";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Home",
    pattern: "Home",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "PostRecipe",
    pattern: "Home/PostRecipe",
    defaults: new { controller = "Home", action = "PostRecipe" });

app.MapControllerRoute(
    name: "MyRecipe",
    pattern: "MyRecipe",
    defaults: new { controller = "MyRecipe", action = "MyRecipe" });

app.MapControllerRoute(
    name: "viewRecipe",
    pattern: "Home/ViewRecipe/{recipeId}",
    defaults: new { controller = "Home", action = "ViewRecipe" });

app.MapControllerRoute(
    name: "viewRecipe",
    pattern: "MyRecipe/ViewRecipe/{recipeId}",
    defaults: new { controller = "MyRecipe", action = "ViewRecipe" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

SeedData.EnsurePopulated(app);

app.Run();
