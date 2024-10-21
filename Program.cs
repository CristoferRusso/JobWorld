using JobWorld.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JobWorld.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi servizi al contenitore.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); // <- Qui aggiungi i controller
builder.Services.AddScoped<JobOfferService>();


// Configurazione DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Aggiungi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:8081") // URL del frontend
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Configura l'autenticazione JWT
var key = Encoding.ASCII.GetBytes("ChiaveSuperSegreta12345"); // Chiave segreta per firmare il token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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

// Usa autenticazione
app.UseAuthentication();

app.UseRouting();

// **Non è necessario modificare questa parte, va bene**
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Questa linea è corretta
});

app.MapGet("/", () => "Il backend è attivo");

// Aggiungi la rotta di login
app.MapPost("/api/user/login", (JobWorld.Models.User user) =>
{
    // Verifica se le credenziali sono corrette (simulazione)
    if (user.Email == "test@example.com" && user.PasswordHash == "password")
    {
        // Genera il token JWT
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes("ChiaveSuperSegreta12345");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Results.Ok(new { Token = tokenString });
    }

    return Results.Unauthorized();
}).WithName("LoginUser");

app.Run("http://0.0.0.0:5000");
