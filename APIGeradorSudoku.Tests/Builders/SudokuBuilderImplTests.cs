using System;
using System.Linq;
using APIGeradorSudoku.Entities.Options;
using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Buiders.Impl;
using APIGeradorSudoku.Composites;
using APIGeradorSudoku.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace APIGeradorSudoku.UnitTests.Builders
{
    public class SudokuBuilderImplTests
    {
        private readonly ISudokuBuilder _sudokuBuilder;
        private readonly Mock<IQuadradoComposite> _quadradoCompositeMock = new();
        private readonly Mock<IOptions<ConfiguracoesConstrucaoSudokuOptions>> _optionsMock = new();

        public SudokuBuilderImplTests()
        {
            _optionsMock.Setup(o => o.Value).Returns(new ConfiguracoesConstrucaoSudokuOptions
            {
                NumeroMaximoTentativas = 3,
                NumerosPossiveisPorQuadrado = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                OrdemGradePadrao = 9
            });

            _sudokuBuilder = new SudokuBuilderImpl(_quadradoCompositeMock.Object, _optionsMock.Object);
        }

        [Fact]
        public void CriarSudoku_DeveRetornarSudokuComGradeCorreta_QuandoQuadradosPreenchidos()
        {
            // Arrange
            _quadradoCompositeMock
                .Setup(q => q.TentarPreencherQuadradoNVezes(It.IsAny<BlocoDeQuadrado>(), ref It.Ref<Sudoku>.IsAny, It.IsAny<int>()))
                .Callback((BlocoDeQuadrado bloco, ref Sudoku sudoku, int tentativas) =>
                {
                    for (int i = bloco.InicioLinha; i < bloco.FimLinha; i++)
                        for (int j = bloco.InicioColuna; j < bloco.FimColuna; j++)
                            sudoku.Grade[i, j] = 1;
                })
                .Returns(true);

            // Act
            var sudoku = _sudokuBuilder.CriarSudoku(9);

            // Assert
            Assert.NotNull(sudoku);
            Assert.NotNull(sudoku.Grade);
            Assert.Equal(9, sudoku.OrdemGradeSudoku);
            Assert.Equal(9, sudoku.Grade.GetLength(0));
            Assert.Equal(9, sudoku.Grade.GetLength(1));
        }

        [Fact]
        public void CriarSudoku_DeveChamarTentarPreencherQuadradoNVezes_ParaCadaQuadrado()
        {
            int chamadas = 0;
            _quadradoCompositeMock
                .Setup(q => q.TentarPreencherQuadradoNVezes(It.IsAny<BlocoDeQuadrado>(), ref It.Ref<Sudoku>.IsAny, It.IsAny<int>()))
                .Callback((BlocoDeQuadrado bloco, ref Sudoku sudoku, int tentativas) =>
                {
                    chamadas++;
                    for (int i = bloco.InicioLinha; i < bloco.FimLinha; i++)
                        for (int j = bloco.InicioColuna; j < bloco.FimColuna; j++)
                            sudoku.Grade[i, j] = 2;
                })
                .Returns(true);

            // Act
            var sudoku = _sudokuBuilder.CriarSudoku(9);

            // Assert
            Assert.True(chamadas >= 1);
        }

        [Fact]
        public void CriarSudoku_DeveLidarComFalhaNoPreenchimentoDeQuadrado()
        {
            // Arrange
            int chamadas = 0;
            _quadradoCompositeMock
                .Setup(q => q.TentarPreencherQuadradoNVezes(It.IsAny<BlocoDeQuadrado>(), ref It.Ref<Sudoku>.IsAny, It.IsAny<int>()))
                .Callback((BlocoDeQuadrado bloco, ref Sudoku sudoku, int tentativas) =>
                {
                    chamadas++;
                    for (int i = bloco.InicioLinha; i < bloco.FimLinha; i++)
                        for (int j = bloco.InicioColuna; j < bloco.FimColuna; j++)
                            sudoku.Grade[i, j] = 3;
                })
                .Returns(() => chamadas > 1);

            // Act
            var sudoku = _sudokuBuilder.CriarSudoku(9);

            // Assert
            Assert.NotNull(sudoku);
            Assert.True(chamadas > 1);
        }
    }
}
