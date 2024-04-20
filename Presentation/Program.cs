#region Usings

using Application.Services.implements.AdminSide;
using Application.Services.implements.SitteSide;
using Application.Services.Interfaces.AdminSide;
using Application.Services.Interfaces.UserSide;
using Data.Repository.GamePart;
using Data.Repository.HomeRepsitory;
using Data.Repository.UserPart;
using Data.ShopDbcontext;
using Domain.IRepository.GamePart;
using Domain.IRepository.HomeRepositoryInterface;
using Domain.IRepository.UserPart;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
#endregion
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region ioc Container
//Home
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IHomeRepository , HomeRepository>();

// AdminGameDetailsViewModel 
builder.Services.AddScoped<IGameRepository, GameRepository>();

#region Store part
// Store
builder.Services.AddScoped<IStoreService, StoreService>();

// catalog 

builder.Services.AddScoped<ICatalogService, CatalogService>();

// Product 
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICommentRepository , CommentRepository>();   

// Genre 
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IgenreService  , GenreService>();

// Platform
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IplatfromsService, PlatfromsService>();

#endregion


#region Account 
//Account 
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService , AccountService>();

//Role
builder.Services.AddScoped<IRoleRepository , RolesRepository>();
builder.Services.AddScoped<IRoleService ,  RoleService>();  


// Cart 
builder.Services.AddScoped<ICartRepository , CartRepository>();
builder.Services.AddScoped<ICartService , CartService>();


#endregion


#region Admin side
// admin Layout
builder.Services.AddScoped<ILayoutService , LayoutService>();

// Dashboard 
builder.Services.AddScoped<IAdminHomeService , AdminHomeService>();

// orders 
builder.Services.AddScoped<IOrdersService , OrdersService>();   

#region Users
builder.Services.AddScoped<IUserService, UsersService>();


//admins 
builder.Services.AddScoped<IAdminService , AdminService>();
#endregion

#endregion

#endregion

#region DbContext
builder.Services.AddDbContext<GameShopDbContext>(
    option => option.UseSqlServer
    (builder.Configuration.GetConnectionString("GameShopDbContextConnectionString")));
#endregion


#region cookies
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
         // Add Cookie settings
         .AddCookie(options =>
         {
             options.LoginPath = "/Account/Signin";
             options.LogoutPath = "/Signout";
             options.ExpireTimeSpan = TimeSpan.FromDays(30);
         });
#endregion


var app = builder.Build();

#region App Services
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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();

#endregion

