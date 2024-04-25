using backend.Services;
using backend.Logica;
using backend.DatosEnMemoria;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    // Configura más opciones si es necesario
});

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<SupabaseService>();
builder.Services.AddScoped<LogicaClase>();
builder.Services.AddScoped<Interfaz, SupabaseService>();
builder.Services.AddScoped<InterfazLogica, LogicaClase>();
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

//app.UseHttpsRedirection();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            
app.UseAuthorization();

app.MapControllers();


app.Run();