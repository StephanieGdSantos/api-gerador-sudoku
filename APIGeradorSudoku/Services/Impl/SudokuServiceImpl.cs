using APIGeradorSudoku.Entities.Options;
using APIGeradorSudoku.Providers;
using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Solvers;
using Microsoft.Extensions.Options;
using System;

namespace APIGeradorSudoku.Services.Impl
{
    public class SudokuServiceImpl(ISudokuBuilder sudokuBuilder, 
        ISudokuSolver sudokuResolver, 
        IRandomProvider randomProvider,
        IOptions<QuantidadeMaximaQuadradosEmBrancoPorNivelOptions>
        quantidadeMaximaQuadradosEmBrancoPorNivelOptions,
        IOptions<ConfiguracoesConstrucaoSudokuOptions>
        configuracoesConstrucaoSudokuOptions) : ISudokuService
    {
        private readonly ISudokuBuilder _sudokuBuilder = sudokuBuilder;
        private readonly ISudokuSolver _sudokuSolver = sudokuResolver;
        private readonly IRandomProvider _randomProvider = randomProvider;

        private readonly QuantidadeMaximaQuadradosEmBrancoPorNivelOptions
            _quantidadeMaximaQuadradosEmBrancoPorNivelOptions =
            quantidadeMaximaQuadradosEmBrancoPorNivelOptions.Value;

        private readonly ConfiguracoesConstrucaoSudokuOptions 
            _configuracoesConstrucaoSudokuOptions = configuracoesConstrucaoSudokuOptions.Value;

        public Sudoku CriarGradeDeSudokuJogavel(NivelEnum nivelDificuldade)
        {
            var quantidadeQuadradosEmBranco = DefinirQuantidadeDeQuadradosEmBrancoPorNivel(nivelDificuldade);

            var ordemGradePadrao = _configuracoesConstrucaoSudokuOptions
                .OrdemGradePadrao;

            var posicoes = Enumerable.Range(0, ordemGradePadrao)
                .SelectMany(l => Enumerable.Range(0, ordemGradePadrao).Select(c => (l, c)))
                .OrderBy(_ => _randomProvider.Next(int.MaxValue))
                .Take(quantidadeQuadradosEmBranco);

            var sudoku = _sudokuBuilder.CriarSudoku(ordemGradePadrao);

            foreach (var (linha, coluna) in posicoes)
            {
                int valorOriginal = sudoku.Grade[linha, coluna];
                sudoku.Grade[linha, coluna] = 0;

                int solucoes = _sudokuSolver.ContarSolucoes((int[,])sudoku.Grade.Clone());
                if (solucoes != 1)
                {
                    sudoku.Grade[linha, coluna] = valorOriginal;
                }
            }

            return sudoku;
        }

        private int DefinirQuantidadeDeQuadradosEmBrancoPorNivel(NivelEnum nivelDificuldade)
        {
            var dicQuantidadeMaximaQuadradosEmBrancoPorNivel = new Dictionary<NivelEnum, int>()
            {
                { NivelEnum.Facil, _quantidadeMaximaQuadradosEmBrancoPorNivelOptions.Facil},
                { NivelEnum.Medio, _quantidadeMaximaQuadradosEmBrancoPorNivelOptions.Medio },
                { NivelEnum.Dificil, _quantidadeMaximaQuadradosEmBrancoPorNivelOptions.Dificil }
            };

            var quantidadeQuadradosEmBranco = _randomProvider
                .Next(
                dicQuantidadeMaximaQuadradosEmBrancoPorNivel[nivelDificuldade] - 10,
                dicQuantidadeMaximaQuadradosEmBrancoPorNivel[nivelDificuldade]);

            return quantidadeQuadradosEmBranco;
        }
    }
}
