using static IdentityServer4.Config;
using static IdentityServerHost.Quickstart.UI.TestUsers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(IdentityResources)
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(ApiScopes)
    .AddInMemoryClients(Clients)
    .AddTestUsers(Users);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();


app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
