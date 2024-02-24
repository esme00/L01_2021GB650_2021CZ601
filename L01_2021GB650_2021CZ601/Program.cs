using Microsoft.EntityFrameworkCore;
using L01_2021GB650_2021CZ601.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyecci�n por dependencia del string de conexion al contexto
builder.Services.AddDbContext<platosContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("restauranteDbConnection")
        )
);
builder.Services.AddDbContext<clientesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("restauranteDbConnection")
        )
);
builder.Services.AddDbContext<pedidosContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("restauranteDbConnection")
        )
);
builder.Services.AddDbContext<motoristasContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("restauranteDbConnection")
        )
);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
