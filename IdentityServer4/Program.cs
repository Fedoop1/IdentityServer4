using IdentityServer4;
using static IdentityServer4.Config;
using static IdentityServerHost.Quickstart.UI.TestUsers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(IdentityResources)
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(ApiScopes)
    .AddInMemoryClients(Clients)
    .AddTestUsers(Users);

builder.Services.AddAuthentication().AddGoogle("Google", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    // Use UserSecrets or Azure KeyVault
    options.ClientId = builder.Configuration.GetSection("GoogleIdentity")["clientId"];
    options.ClientSecret = builder.Configuration.GetSection("GoogleIdentity")["clientId"];
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();


app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
