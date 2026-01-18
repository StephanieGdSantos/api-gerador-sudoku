using APIGeradorSudoku.Solvers.Impl;
using Xunit;

namespace APIGeradorSudoku.UnitTests.Solvers
{
    public class SudokuSolverImplTests
    {
        private readonly SudokuSolverImpl _solver;

        public SudokuSolverImplTests()
        {
            _solver = new SudokuSolverImpl();
        }

        [Fact]
        public void ContarSolucoes_DeveRetornar1_ParaSudokuResolvido()
        {
            // Arrange
            var grade = new int[2, 2]
            {
                { 1, 2 },
                { 2, 1 }
            };

            // Act
            var solucoes = _solver.ContarSolucoes(grade);

            // Assert
            Assert.Equal(1, solucoes);
        }

        [Fact]
        public void ContarSolucoes_DeveRetornarMaisQue1_ParaSudokuComMultiplasSolucoes()
        {
            // Arrange
            var grade = new int[2, 2]
            {
                { 0, 0 },
                { 0, 0 }
            };

            // Act
            var solucoes = _solver.ContarSolucoes(grade);

            // Assert
            Assert.True(solucoes > 1);
        }

        [Fact]
        public void ContarSolucoes_DeveRetornar1_ParaSudokuVazioComUnicaSolucao()
        {
            // Arrange
            var grade = new int[1, 1]
            {
                { 0 }
            };

            // Act
            var solucoes = _solver.ContarSolucoes(grade);

            // Assert
            Assert.Equal(1, solucoes);
        }
    }
}
