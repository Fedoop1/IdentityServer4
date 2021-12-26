using static IdentityServer4.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(ApiScopes)
    .AddInMemoryClients(Clients);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseIdentityServer();

app.Run();
