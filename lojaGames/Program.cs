using FluentValidation;
using lojaGames.Data;
using lojaGames.Model;
using lojaGames.Service;
using lojaGames.Service.Implements;
using lojaGames.Validator;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Conexão com o Banco de dados
var connectionString = builder.Configuration.
    GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// registrar validações do banco de dados -NEW
builder.Services.AddTransient<IValidator<Produto>, ProdutoValidator>();
builder.Services.AddTransient<IValidator<Categoria>, CategoriaValidator>();

//Registrar as classes de serviço (SERVICE)
builder.Services.AddScoped<IProdutoService, ProdutoService> ();
builder.Services.AddScoped<ICategoriaService, CategoriaService> ();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuração do CORS
builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Criar o Banco de dados e as tabelas Automaticamente
using (var scope = app.Services.CreateAsyncScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();
    }

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
