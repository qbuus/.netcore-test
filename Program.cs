using API;
using API.entityFramework;
using API.services;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeedercs>();
builder.Services.AddAutoMapper(this.GetType().Assembly);
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<Middleware>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
