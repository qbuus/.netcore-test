using API;
using API.entityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeedercs>();
builder.Services.AddAutoMapper(this.GetType().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
