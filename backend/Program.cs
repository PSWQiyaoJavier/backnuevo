/*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using backend.Logica;
using backend.Models;
using backend.Services;
using backend.Controllers;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var supabaseService = services.GetRequiredService<SupabaseService>();
            var logica = services.GetRequiredService<Logica>();

            // Aquí puedes llamar a métodos de SupabaseService y Logica para comprobar su funcionalidad
            // Por ejemplo:
            supabaseService.InitializeSupabaseAsync().Wait();
            Console.WriteLine("Supabase inicializado correctamente.");
            /*
            Usuario user = new Usuario("javi","pedro99","contra1","javi@ejemplo.com",45);
            logica.AddMember(user);
            user.Edad = 45;
            logica.
            /*
            Usuario buyer1 = logica.ObtenerUsuarioPorNick("pedro99");
            Producto prod1 = logica.ObtenerProductoPorPrecio(5000);
            logica.AgregarAlCarrito(buyer1.Id,prod1.Id);
            

            
            
            Usuario user1 = logica.ObtenerUsuarioPorNick("maria87");

            Console.WriteLine("Id de maria : " + user1.Id);
            Console.WriteLine("Edad de maria : " + user1.Edad);
            user1 = logica.UpdateEdadUsuario(user1,85);
            Console.WriteLine("Edad de maria : " + user1.Edad);
            Usuario user2 = logica.ObtenerUsuarioPorEdad(85);
            Console.WriteLine("user2 : " + user2.Nombre);
            Console.WriteLine("user2 : " + user2.Edad);
            Console.WriteLine("user1 : " + user1.Edad);
            


            //logica.UpdateUsuario(user1);
            Console.WriteLine(user1.Nombre);
            // Realiza otras operaciones como insertar productos, obtener usuarios, etc.
        }

        Configure(app);
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<SupabaseService>();
        services.AddScoped<Logica>();
        services.AddScoped<Interfaz, SupabaseService>();
    }

    private static void Configure(WebApplication app)
    {
        app.UseRouting();

        app.MapControllers();


        app.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("¡La API está en funcionamiento!");
        });
    }
    
}


/*

using backend.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

*/