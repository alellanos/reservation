using Microsoft.EntityFrameworkCore;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Application.Interfaces;
using ReservationManagement.Application.UseCases.CreateReservation;
using ReservationManagement.Infrastructure.Persistence;
using ReservationManagement.Infrastructure.Repositories;
using UserSystem.Infrastructure;
using ReservationManagement.Application.UseCases.CancelReservation;
using ReservationManagement.Application.UseCases.ConfirmReservation;
using ReservationManagement.Application.UseCases.GetReservationDetail;
using ReservationManagement.Application.UseCases.GetReservations;
using ReservationManagement.Application.UseCases.MarkReservationAsNoShow;
using System.Text.Json.Serialization;
using ReservationManagement.Application.UseCases.CompleteReservation;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ReservationDbContext>(options =>
//{
//    var cs = builder.Configuration.GetConnectionString("prdaws");

//    options.UseMySql(
//        cs,
//        ServerVersion.AutoDetect(cs)
//    );
//});

builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("prdaws"),
    new MySqlServerVersion(new Version(8, 0, 31))));


//builder.Services.AddControllers();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICreateReservationUseCase, CreateReservationUseCase>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<ICreateReservationUseCase, CreateReservationUseCase>();
builder.Services.AddScoped<IGetReservationsUseCase, GetReservationsUseCase>();
builder.Services.AddScoped<IGetReservationDetailUseCase, GetReservationDetailUseCase>();
builder.Services.AddScoped<IConfirmReservationUseCase, ConfirmReservationUseCase>();
builder.Services.AddScoped<ICancelReservationUseCase, CancelReservationUseCase>();
builder.Services.AddScoped<IMarkReservationAsNoShowUseCase, MarkReservationAsNoShowUseCase>();
builder.Services.AddScoped<ICompleteReservationUseCase, CompleteReservationUseCase>();

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

app.UseCors("DevCors");

app.Run();
