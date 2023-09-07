using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.CRUD;
using DataAccessLayer.Repositories;
using HastaKayýt.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SONRADAN EKLENENLER
builder.Services.AddDbContext<RepositoryContex>();
builder.Services.AddScoped<crud>();
builder.Services.AddSingleton<ILoggerService, LoggerManager>();
builder.Services.AddScoped<IHastaService, HastaService>();


var app = builder.Build();

var logger = app.Services.GetService<ILoggerService>();
app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
