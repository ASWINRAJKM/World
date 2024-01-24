using Microsoft.EntityFrameworkCore;
using WorldAPI.Common;
using WorldAPI.Data;
using WorldAPI.Repository;
using WorldAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure CORS
builder.Services.AddCors(options=>
{
    options.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion

#region Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
#endregion

#region Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region Configure Country Dependency Injection
builder.Services.AddTransient<ICountryRepository,CountryRepository>();
#endregion

#region Configure States Dependency Injection
builder.Services.AddTransient<IStatesRepository, StatesRepository>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
