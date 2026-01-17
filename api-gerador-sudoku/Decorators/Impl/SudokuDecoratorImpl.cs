using api_gerador_sudoku.Options;
using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Resolvers;
using Microsoft.Extensions.Options;
using System;

namespace APIGeradorSudoku.Decorators.Impl
{
    public class SudokuDecoratorImpl(ISudokuBuilder sudokuBuilder, 
        ISudokuSolver sudokuResolver, 
        IOptions<QuantidadeMaximaQuadradosEmBrancoPorNivelOptions>
        quantidadeMaximaQuadradosEmBrancoPorNivelOptions,
        IOptions<ConfiguracoesConstrucaoSudokuOptions>
        configuracoesConstrucaoSudokuOptions) : ISudokuDecorator
    {
        private readonly ISudokuBuilder _sudokuBuilder = sudokuBuilder;
        private readonly ISudokuSolver _sudokuSolver = sudokuResolver;
        private readonly QuantidadeMaximaQuadradosEmBrancoPorNivelOptions
            _quantidadeMaximaQuadradosEmBrancoPorNivelOptions =
            quantidadeMaximaQuadradosEmBrancoPorNivelOptions.Value;
        private readonly ConfiguracoesConstrucaoSudokuOptions 
            _configuracoesConstrucaoSudokuOptions = configuracoesConstrucaoSudokuOptions.Value;
        private readonly Random _random = new();

        public Sudoku CriarGradeDeSudokuJogavel(NivelEnum nivelDificuldade)
        {
            var quantidadeQuadradosEmBranco = DefinirQuantidadeDeQuadradosEmBrancoPorNivel(nivelDificuldade);

            var posicoes = Enumerable.Range(0, 9)
                .SelectMany(l => Enumerable.Range(0, 9).Select(c => (l, c)))
                .OrderBy(_ => _random.Next())
                .Take(quantidadeQuadradosEmBranco);

            var sudoku = _sudokuBuilder.CriarSudoku(_configuracoesConstrucaoSudokuOptions
                .OrdemGradePadrao);

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

            var quantidadeQuadradosEmBranco = _random
                .Next(
                dicQuantidadeMaximaQuadradosEmBrancoPorNivel[nivelDificuldade] - 15,
                dicQuantidadeMaximaQuadradosEmBrancoPorNivel[nivelDificuldade]);

            return quantidadeQuadradosEmBranco;
        }
    }
}
