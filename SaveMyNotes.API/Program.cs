using SaveMyNotes.Application;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Infrastructure.Services.Identity;
using SaveMyNotes.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Katman Servisleri
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// --- BURAYA DİKKAT: JWT/Identity Ayarları Buraya Gelecek ---
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(...);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();
app.Run();