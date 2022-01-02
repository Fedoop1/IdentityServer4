using IdentityServer4;
using IdentityServer4.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static IdentityServerHost.Quickstart.UI.TestUsers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddTestUsers(Users)
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = callback =>
            callback.UseSqlServer(
                builder.Configuration.GetConnectionString("ConfigurationDbConnectionString"), 
                dbOptions => dbOptions.MigrationsAssembly(typeof(Program).Assembly.FullName));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = callback =>
            callback.UseSqlServer(builder.Configuration.GetConnectionString("PersistedGrantDbConnectionString"),
                dbOptions => dbOptions.MigrationsAssembly(typeof(Program).Assembly.FullName));
    });


builder.Services.AddAuthentication().AddGoogle("Google", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    // Use UserSecrets or Azure KeyVault
    options.ClientId = builder.Configuration.GetSection("GoogleIdentity")["clientId"];
    options.ClientSecret = builder.Configuration.GetSection("GoogleIdentity")["clientSecret"];
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.InitializeDatabaseAsync();

app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();


