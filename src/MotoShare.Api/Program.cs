using MotoShare.Api;
using MotoShare.Api.Configuration;
using MotoShare.Domain;
using MotoShare.Infrastructure;
using MotoShare.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    // aplica retorno padrão para a aplicação
    options.InvalidModelStateResponseFactory = context =>
    {
        return context.ConfigureResult();
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura infra
builder.Services.ConfigureInfrastructure(builder.Configuration, builder.Logging);
builder.Services.ConfigureDomain();

// Configura serviços da aplicação
builder.AddMongoConfiguration();
builder.AddMediatRConfiguration();
builder.AddFluentValidationConfiguration();
builder.AddSwaggerConfiguration();

var app = builder.Build();

// Middleware que padroniza retorno de erros
app.UseMiddleware<ErrorHandlerMiddleware>();

// Adiciona escuta da fila
app.AddRabbitMqSubscriber();

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
