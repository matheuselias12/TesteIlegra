using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TesteIlegra.API.Config;
using TesteIlegra.Data;
using TesteIlegra.Repositorio.Interface;
using TesteIlegra.Repositorio;
using TesteIlegra.Service;
using TesteIlegra.Service.Interface;
using Polly;
using Polly.CircuitBreaker;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BancoDeDados")));

        builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RevendaValidator>());
        builder.Services.AddHttpClient("Vebma", c =>
        {
            c.BaseAddress = new Uri("https://localhost:5000/mock/vebma"); // mock
        })
        .AddTransientHttpErrorPolicy(p =>
            p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
        .AddTransientHttpErrorPolicy(p =>
            p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(10)))
        .AddPolicyHandler(Policy<HttpResponseMessage>
            .Handle<BrokenCircuitException>()
            .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("Serviço da Ambev indisponível. Circuito aberto.")
            }));

        builder.Services.AddScoped<IRevendaRepositorio, RevendaRepositorio>();
        builder.Services.AddScoped<IRevendaServico, RevendaServico>();
        builder.Services.AddScoped<IVebmaServico, VebmaServicoHttp>();

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
    }
}