using APIGeradorSudoku.DTOs;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Mappers;
using Xunit;

namespace APIGeradorSudoku.UnitTests.Mappers
{
    public class SudokuMapperTests
    {

        [Fact]
        public void ToDTO_DeveMapearCorretamente_SudokuParaDTO()
        {
            // Arrange
            var grade = new int[2, 2] { { 1, 2 }, { 3, 4 } };
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = 2,
                Grade = grade
            };

            // Act
            var dto = SudokuMapper.ToDTO(sudoku);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(sudoku.OrdemGradeSudoku, dto.OrdemGradeSudoku);
            Assert.Equal(sudoku.OrdemQuadradoSudoku, dto.OrdemQuadradoSudoku);
            Assert.Equal(grade[0, 0], dto.Grade[0][0]);
            Assert.Equal(grade[0, 1], dto.Grade[0][1]);
            Assert.Equal(grade[1, 0], dto.Grade[1][0]);
            Assert.Equal(grade[1, 1], dto.Grade[1][1]);
        }

        [Fact]
        public void ConverterParaArrayDeArrays_DeveConverterCorretamente_MatrizParaArrayDeArrays()
        {
            // Arrange
            var matriz = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

            // Act
            var resultado = SudokuMapper.ConverterParaArrayDeArrays(matriz);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Length);
            Assert.Equal(3, resultado[0].Length);
            Assert.Equal(3, resultado[1].Length);
            Assert.Equal(matriz[0, 0], resultado[0][0]);
            Assert.Equal(matriz[0, 1], resultado[0][1]);
            Assert.Equal(matriz[0, 2], resultado[0][2]);
            Assert.Equal(matriz[1, 0], resultado[1][0]);
            Assert.Equal(matriz[1, 1], resultado[1][1]);
            Assert.Equal(matriz[1, 2], resultado[1][2]);
        }

        [Fact]
        public void ToDTO_DeveRetornarGradeVazia_QuandoSudokuGradeVazia()
        {
            // Arrange
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = 0,
                Grade = new int[0, 0]
            };

            // Act
            var dto = SudokuMapper.ToDTO(sudoku);

            // Assert
            Assert.NotNull(dto);
            Assert.NotNull(dto.Grade);
            Assert.Empty(dto.Grade);
        }
    }
}
