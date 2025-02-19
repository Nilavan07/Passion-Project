using FootballHub.Data;
using FootballHub.Interface;
using FootballHub.Models;
using FootballHub.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Database Context (MySQL)
string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FootballHubContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

// ✅ Configure Identity Services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<FootballHubContext>();
builder.Services.AddScoped<IMatch, FootballHub.Services.Match>();
builder.Services.AddScoped<IPlayer, FootballHub.Services.Player>();
builder.Services.AddScoped<ITeam, FootballHub.Services.Team>();


// ✅ Add Controllers & Swagger
builder.Services.AddControllersWithViews();

//builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FootballHub API",
        Version = "v1",
        Description = "API for managing football teams, players, and matches."
    });
});

var app = builder.Build();

// ✅ Ensure Static Files Load
app.UseStaticFiles();

// ✅ Enable HTTPS, Routing, Authentication & Authorization
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ✅ Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FootballHub API v1");
        c.RoutePrefix = "swagger"; // ✅ Swagger will only be accessible at `/swagger`
    });
}


// ✅ Correct Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
app.MapRazorPages();

app.Run();
