using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:5001";

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("fullScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "IdentityServer4Provider");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization("fullScope");

app.Run();
