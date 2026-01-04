using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Resolvers;
using System;

namespace APIGeradorSudoku.Decorators.Impl
{
    public class SudokuDecoratorImpl(ISudokuBuilder sudokuBuilder, ISudokuSolver sudokuResolver) : ISudokuDecorator
    {
        private readonly ISudokuBuilder _sudokuBuilder = sudokuBuilder;
        private readonly ISudokuSolver _sudokuSolver = sudokuResolver;
        private readonly Random _random = new Random();

        public Sudoku CriarGradeDeSudokuJogavel(NivelEnum nivelDificuldade, int ordemGrade = 9)
        {
            var dicQuantidadeMaximaQuadradosEmBrancoPorNivel = new Dictionary<NivelEnum, int>()
            {
                { NivelEnum.Facil, 20 },
                { NivelEnum.Medio, 35 },
                { NivelEnum.Dificil, 45 }
            };

            var quantidadeQuadradosEmBranco = new Random().Next(
                dicQuantidadeMaximaQuadradosEmBrancoPorNivel[nivelDificuldade] - 15,
                dicQuantidadeMaximaQuadradosEmBrancoPorNivel[nivelDificuldade]);

            var posicoes = Enumerable.Range(0, 9)
                .SelectMany(l => Enumerable.Range(0, 9).Select(c => (l, c)))
                .OrderBy(_ => _random.Next())
                .Take(quantidadeQuadradosEmBranco);

            var sudoku = _sudokuBuilder.CriarSudoku(ordemGrade);

            /*foreach (var (linha, coluna) in posicoes)
                sudoku.Grade[linha, coluna] = 0;*/

            foreach (var (linha, coluna) in posicoes)
            {
                int valorOriginal = sudoku.Grade[linha, coluna];
                sudoku.Grade[linha, coluna] = 0;

                // Usa o resolvedor para garantir que o Sudoku ainda tem solução única
                int solucoes = _sudokuSolver.ContarSolucoes((int[,])sudoku.Grade.Clone());
                if (solucoes != 1)
                {
                    sudoku.Grade[linha, coluna] = valorOriginal; // Reverte se não for única
                }
            }

            return sudoku;
        }
    }
}
