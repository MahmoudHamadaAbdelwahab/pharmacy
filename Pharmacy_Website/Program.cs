using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dominos.Models;
using Dominos.Bl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Bl;

var builder = WebApplication.CreateBuilder(args);

// Use MVC design pattern
builder.Services.AddControllersWithViews();

#region Entity Framework
// Add the DbContext and specify the provider
builder.Services.AddDbContext<PharmacyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Use Identity framework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<PharmacyContext>();
#endregion

#region Custom Services
// Register custom services
builder.Services.AddScoped<IProducts, ClsProducts>();
builder.Services.AddScoped<Icustomer, ClsCustomer>();
builder.Services.AddScoped<IPharmicsts, ClsPharmicsts>();
builder.Services.AddScoped<ISupplier, ClsSuppliers>();
builder.Services.AddScoped<IClassification, ClsClassification>();
builder.Services.AddScoped<ISettings, ClsSettings>();
builder.Services.AddScoped<ISliders, ClsSlider>();
builder.Services.AddScoped<IImages, ClsImages>();

#endregion

// Add session services
#region Sesstion and cookies
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    options.LoginPath = "/Users/Login"; // the Users it's some controller and the login & AccessDenied it's inside view 
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});
#endregion

// add swagger decumentaion api
#region Add Swagger
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "v1",
            Title = "PharmacyApi",
            Description = "api to access items",
            TermsOfService = new Uri("https://stackoverflow.com/"),
            Contact = new OpenApiContact
            {
                Email = "info@pharmacy.net",
                Name = "mahmoud mahmoud",
                Url = new Uri("https://stackoverflow.com/")

            },
            License = new OpenApiLicense
            {
                Name = "pharmacy owner website mahmoud hamada",
                Url = new Uri("https://www.linkedin.com/feed/")
            },
        });

        var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
        options.IncludeXmlComments(fullPath);

    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. Consider changing this for production scenarios.
    app.UseHsts();
}

#region Swagger UI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Configure routing
#region Routing
    app.UseEndpoints(endpoints =>
    {
        // Area routing
    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        // Default routing
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    }); 
#endregion

app.Run();
