using System.Text;
using API;
using API.entityFramework;
using API.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var authSettings = new AuthSettings();


// Add services to the container
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("Authentication"));

builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = authSettings.JwtIssuer,
        ValidAudience = authSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey))
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));
    options.AddPolicy("AtLeast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
    options.AddPolicy("HasAtLeast2Restaurants", builder => builder.AddRequirements(new MinimRestaurantCount(2)));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeedercs>();
builder.Services.AddAutoMapper(this.GetType().Assembly);
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<Middleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MiddlewareClass>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddSingleton(authSettings);

// NLogger
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseMiddleware<Middleware>();
app.UseMiddleware<MiddlewareClass>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
