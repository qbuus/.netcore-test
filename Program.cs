using System.Text;
using API;
using API.entityFramework;
using API.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings")));
builder.Services.AddScoped<RestaurantSeedercs>();
builder.Services.AddAutoMapper(this.GetType().Assembly);
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<Middleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MiddlewareClass>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddSingleton(authSettings);
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendClient", builder => builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://locahlost:8080"));
});  


// NLogger
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("FrontendClient");

app.UseMiddleware<Middleware>();
app.UseMiddleware<MiddlewareClass>();

app.UseAuthentication();

app.UseResponseCaching();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
