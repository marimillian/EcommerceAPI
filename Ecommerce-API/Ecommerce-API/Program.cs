using Ecommerce_API.Data;
using Ecommerce_API.Middlewares;
using Ecommerce_API.Repository;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//o builder serve para fazer a conexão ao banco

var connectionString = builder.Configuration.GetConnectionString("EcommerceAPIConnection");

builder.Services.AddSerilog();

string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{ambiente}.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
try
{
    Log.Information("Iniciando Ecommerce API");
}

catch(Exception e)
{
    Log.Fatal(e, "Ocorreu um Erro Inesperado");
    
}


builder.Services.AddDbContext<EcommerceContext>(opts => opts.UseLazyLoadingProxies().
UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder para utilizar o AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ISubcategoriaService, SubcategoriaService>();
builder.Services.AddScoped<ISubcategoriaRepository, SubcategoriaRepository>();
builder.Services.AddScoped<ICentroDistribuicaoService, CentroDistribuicaoService>();
builder.Services.AddScoped<ICentroDistribuicaoRepository, CentroDistribuicaoRepository>();
builder.Services.AddScoped<ICarrinhoComprasRepository, CarrinhoComprasRepository>();
builder.Services.AddScoped<ICarrinhoComprasService, CarrinhoComprasService>();
builder.Services.AddScoped<IPagamentoCompraService, PagamentoCompraService>();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(token =>
    {
        token.RequireHttpsMetadata = false;
        token.SaveToken = true;
        token.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });


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

app.UseMiddleware(typeof(ErrorMiddleware));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


