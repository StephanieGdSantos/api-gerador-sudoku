using APIGeradorSudoku.Controllers;
using APIGeradorSudoku.DTOs;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Mappers;
using APIGeradorSudoku.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace APIGeradorSudoku.UnitTests.Controllers
{
    public class SudokuControllerTests
    {
        private readonly Mock<ISudokuService> _sudokuServiceMock;
        private readonly SudokuController _controller;

        public SudokuControllerTests()
        {
            _sudokuServiceMock = new Mock<ISudokuService>();
            _controller = new SudokuController(_sudokuServiceMock.Object);
        }

        [Theory]
        [InlineData((NivelEnum)0)]
        [InlineData((NivelEnum)4)]
        [InlineData((NivelEnum)(-1))]
        public void GetSudokuJogavel_DeveRetornarBadRequest_ParaNivelInvalido(NivelEnum nivel)
        {
            // Act
            var result = _controller.GetSudokuJogavel(nivel);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Nível de dificuldade inválido. Valores permitidos: 1 (Fácil), 2 (Médio), 3 (Difícil).", badRequest.Value);
        }

        [Theory]
        [InlineData(NivelEnum.Facil)]
        [InlineData(NivelEnum.Medio)]
        [InlineData(NivelEnum.Dificil)]
        public void GetSudokuJogavel_DeveRetornarOk_ParaNivelValido(NivelEnum nivel)
        {
            // Arrange
            var sudoku = new Sudoku
            {
                Grade = new int[9, 9],
                OrdemGradeSudoku = 9
            };
            _sudokuServiceMock.Setup(s => s.CriarGradeDeSudokuJogavel(nivel)).Returns(sudoku);

            // Act
            var result = _controller.GetSudokuJogavel(nivel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var dto = Assert.IsType<SudokuDTO>(okResult.Value);
            Assert.Equal(sudoku.OrdemGradeSudoku, dto.OrdemGradeSudoku);
        }
    }
}
