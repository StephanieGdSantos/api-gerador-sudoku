using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Buiders.Impl;
using APIGeradorSudoku.Composites;
using APIGeradorSudoku.Composites.Impl;
using APIGeradorSudoku.Services;
using APIGeradorSudoku.Services.Impl;
using APIGeradorSudoku.Solvers;
using APIGeradorSudoku.Solvers.Impl;
using APIGeradorSudoku.Providers;
using APIGeradorSudoku.Providers.Impl;
using APIGeradorSudoku.Entities.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IQuadradoComposite, QuadradoCompositeImpl>();
builder.Services.AddScoped<ISudokuBuilder, SudokuBuilderImpl>();
builder.Services.AddScoped<ISudokuSolver, SudokuSolverImpl>();
builder.Services.AddScoped<ISudokuService, SudokuServiceImpl>();
builder.Services.AddSingleton<IRandomProvider, RandomProviderImp>();

builder.Services.AddOptions<ConfiguracoesConstrucaoSudokuOptions>()
    .Bind(builder.Configuration
.GetSection("ConfiguracoesConstrucaoSudoku"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<QuantidadeMaximaQuadradosEmBrancoPorNivelOptions>()
    .Bind(builder.Configuration
.GetSection("QuantidadeMaximaQuadradosEmBrancoPorNivel"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

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
