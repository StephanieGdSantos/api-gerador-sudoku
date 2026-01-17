using api_gerador_sudoku.Options;
using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Buiders.Impl;
using APIGeradorSudoku.Composites;
using APIGeradorSudoku.Composites.Impl;
using APIGeradorSudoku.Decorators;
using APIGeradorSudoku.Decorators.Impl;
using APIGeradorSudoku.Resolvers;
using APIGeradorSudoku.Resolvers.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IQuadradoComposite, QuadradoCompositeImpl>();
builder.Services.AddScoped<ISudokuBuilder, SudokuBuilderImpl>();
builder.Services.AddScoped<ISudokuSolver, SudokuSolverImpl>();
builder.Services.AddScoped<ISudokuDecorator, SudokuDecoratorImpl>();

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
