using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Mock;
using Repository.Interface;
using Service.Interface;
using Service.Services;
using Repository.Repositories;
using Service.Algorithm;
using AutoMapper; // ← חובה להוסיף
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
});

// CORS policy
var myAllowSpecificOrigin = "AllowSpecificOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigin,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ✅ הוספת AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // ← הוספת שורה זו

// Add project services and repositories
builder.Services.AddServices();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAlgorithm, DinamicTrack>();
builder.Services.AddScoped<IFitnessExerciseService, FitnessExerciseService>();
builder.Services.AddScoped<IFitnessExerciseRepository, FitnessExerciseRepository>();
builder.Services.AddScoped<ITrackExerciseService, TrackExerciseService>();
builder.Services.AddScoped<ITrackExerciseRepository, TrackExerciseRepository>();

// ✅ הוספת TokenService לשירותים
builder.Services.AddScoped<TokenService>();  // הוספת שורה זו

// Add EF DbContext
builder.Services.AddDbContext<IContext, DataContext>();

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors(myAllowSpecificOrigin);

app.UseHttpsRedirection();

// ✅ Serve static files from wwwroot (images, gifs, etc.)
app.UseStaticFiles();

app.MapControllers();

app.Run();
