using JobWorld.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi servizi al contenitore.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurazione DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Aggiungi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:8081") // Sostituisci con l'URL del tuo frontend
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.WebHost.UseUrls("http://*:5000");

var app = builder.Build();

// Configura la pipeline delle richieste HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa CORS
app.UseCors("AllowSpecificOrigin");

app.MapGet("/", () => "Il backend è attivo e funzionante!");


// Aggiungi la rotta di login
app.MapPost("/api/user/login", (JobWorld.Models.User user) =>
{
    // Logica di autenticazione qui
    return Results.Ok(new { Name = user.Email }); // Simulazione della risposta
}).WithName("LoginUser");


app.Run("http://0.0.0.0:5000");



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
