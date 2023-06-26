using EndPoints.Application.AutoMapper;
using EndPoints.Application.Services;
using EndPoints.Data;
using EndPoints.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(DomainToDtoMapping), typeof(DtoToDomainMapping));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IEndPointRepository, EndPointRepository>();
builder.Services.AddScoped<IEndPointService, EndPointService>();    

builder.Services.AddDbContext<EndPointContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
