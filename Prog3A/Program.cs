using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prog3A.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Added custom Autorisation
builder.Services.AddAuthentication("MyCookieAuth")
.AddCookie("MyCookieAuth", options =>
{
    options.LoginPath = "/Login/Login"; // Redirect to login if not authenticated
});

builder.Services.AddAuthorization();


// Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// usual middleware...
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication/authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add Identity endpoints
app.MapRazorPages();

app.Run();
