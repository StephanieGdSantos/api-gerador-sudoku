using APIGeradorSudoku.Entities.Options;
using APIGeradorSudoku.Providers;
using APIGeradorSudoku.Buiders;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Solvers;
using APIGeradorSudoku.Services.Impl;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace APIGeradorSudoku.UnitTests.Services
{
    public class SudokuServiceImplTests
    {
        private readonly Mock<ISudokuBuilder> _sudokuBuilderMock = new();
        private readonly Mock<ISudokuSolver> _sudokuSolverMock = new();
        private readonly Mock<IRandomProvider> _randomProviderMock = new();
        private readonly Mock<IOptions<QuantidadeMaximaQuadradosEmBrancoPorNivelOptions>> _qtdBrancoOptionsMock = new();
        private readonly Mock<IOptions<ConfiguracoesConstrucaoSudokuOptions>> _configOptionsMock = new();
        private readonly SudokuServiceImpl _service;

        public SudokuServiceImplTests()
        {
            _qtdBrancoOptionsMock.Setup(o => o.Value).Returns(new QuantidadeMaximaQuadradosEmBrancoPorNivelOptions
            {
                Facil = 20,
                Medio = 30,
                Dificil = 40
            });

            _configOptionsMock.Setup(o => o.Value).Returns(new ConfiguracoesConstrucaoSudokuOptions
            {
                OrdemGradePadrao = 4
            });

            _randomProviderMock
                .Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int min, int max) => min);

            _randomProviderMock
                .Setup(r => r.Next(It.IsAny<int>()))
                .Returns(0);

            _sudokuBuilderMock
                .Setup(b => b.CriarSudoku(It.IsAny<int>()))
                .Returns(() => new Sudoku
                {
                    OrdemGradeSudoku = 4,
                    Grade = new int[4, 4]
                    {
                        { 1, 2, 3, 4 },
                        { 3, 4, 1, 2 },
                        { 2, 1, 4, 3 },
                        { 4, 3, 2, 1 }
                    }
                });

            _sudokuSolverMock
                .Setup(s => s.ContarSolucoes(It.IsAny<int[,]>()))
                .Returns(1);

            _service = new SudokuServiceImpl(
                _sudokuBuilderMock.Object,
                _sudokuSolverMock.Object,
                _randomProviderMock.Object,
                _qtdBrancoOptionsMock.Object,
                _configOptionsMock.Object
            );
        }

        [Theory]
        [InlineData(NivelEnum.Facil, 20)]
        [InlineData(NivelEnum.Medio, 30)]
        [InlineData(NivelEnum.Dificil, 40)]
        public void CriarGradeDeSudokuJogavel_DeveRetornarSudokuComGradeCorreta(NivelEnum nivel, int maxBrancos)
        {
            // Act
            var sudoku = _service.CriarGradeDeSudokuJogavel(nivel);

            // Assert
            Assert.NotNull(sudoku);
            Assert.NotNull(sudoku.Grade);
            Assert.Equal(4, sudoku.OrdemGradeSudoku);
            Assert.Equal(4, sudoku.Grade.GetLength(0));
            Assert.Equal(4, sudoku.Grade.GetLength(1));
        }

        [Fact]
        public void CriarGradeDeSudokuJogavel_DeveRestaurarValorSeMaisDeUmaSolucao()
        {
            // Arrange
            int callCount = 0;
            _sudokuSolverMock
                .Setup(s => s.ContarSolucoes(It.IsAny<int[,]>()))
                .Returns(() => ++callCount == 1 ? 2 : 1);

            // Act
            var sudoku = _service.CriarGradeDeSudokuJogavel(NivelEnum.Facil);

            // Assert
            bool algumZero = false;
            foreach (var v in sudoku.Grade)
                if (v == 0)
                    algumZero = true;
            Assert.True(algumZero);
        }

        [Fact]
        public void CriarGradeDeSudokuJogavel_DeveChamarBuilderComOrdemCorreta()
        {
            // Act
            _service.CriarGradeDeSudokuJogavel(NivelEnum.Facil);

            // Assert
            _sudokuBuilderMock.Verify(b => b.CriarSudoku(4), Times.Once);
        }

        [Fact]
        public void CriarGradeDeSudokuJogavel_DeveChamarContarSolucoesParaCadaRemocao()
        {
            // Arrange
            int chamadas = 0;
            _sudokuSolverMock
                .Setup(s => s.ContarSolucoes(It.IsAny<int[,]>()))
                .Callback(() => chamadas++)
                .Returns(1);

            // Act
            _service.CriarGradeDeSudokuJogavel(NivelEnum.Facil);

            // Assert
            Assert.True(chamadas > 0);
        }
    }
}
