using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Data.Repositories;
using PruebaTecniaAB.Models;

var builder = WebApplication.CreateBuilder(args);

//Dependencies 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(
            new ResponseCacheAttribute {
        
                    NoStore = true,
        
            }
        );
});

builder.Services.AddDbContext<DBVENTASContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConection"))

);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Home/Index";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrador"));
    
    options.AddPolicy("AllRoles", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Administrador") || context.User.IsInRole("Seller") || context.User.IsInRole("Accountant")));

    options.AddPolicy("AmdinAndSeller", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Administrador") || context.User.IsInRole("Seller")));

    options.AddPolicy("AmdinAndAccountant", policy =>
           policy.RequireAssertion(context =>
               context.User.IsInRole("Administrador") || context.User.IsInRole("Accountant")));
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
