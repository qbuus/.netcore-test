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
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MiddlewareClass>();

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

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
