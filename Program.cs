using API;
using API.entityFramework;
using API.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeedercs>();
builder.Services.AddAutoMapper(this.GetType().Assembly);
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
