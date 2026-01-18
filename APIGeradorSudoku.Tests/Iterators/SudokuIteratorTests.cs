using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Iterator;
using System.Collections.Generic;
using Xunit;

namespace APIGeradorSudoku.UnitTests.Iterators
{
    public class SudokuIteratorTests
    {

        [Fact]
        public void ObterNumerosJaUtilizadosNaGrade_DeveRetornarNumerosDoQuadradoAtual_QuandoPrimeiraCelula()
        {
            // Arrange
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = 2,
                Grade = new int[2, 2] { { 0, 0 }, { 0, 0 } }
            };
            var numerosQuadradoAtual = new List<int> { 1, 2 };
            int linhaAtual = 0;
            int colunaAtual = 0;

            // Act
            var resultado = SudokuIterator.ObterNumerosJaUtilizadosNaGrade(sudoku, numerosQuadradoAtual, linhaAtual, colunaAtual);

            // Assert
            Assert.Equal(numerosQuadradoAtual, resultado);
        }

        [Fact]
        public void ObterNumerosJaUtilizadosNaGrade_DeveAdicionarNumerosDasLinhasAcima_QuandoLinhaAtualMaiorQueZero()
        {
            // Arrange
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = 2,
                Grade = new int[2, 2] { { 3, 0 }, { 0, 0 } }
            };
            var numerosQuadradoAtual = new List<int> { 1 };
            int linhaAtual = 1;
            int colunaAtual = 0;

            // Act
            var resultado = SudokuIterator.ObterNumerosJaUtilizadosNaGrade(sudoku, numerosQuadradoAtual, linhaAtual, colunaAtual);

            // Assert
            Assert.Contains(1, resultado);
            Assert.Contains(3, resultado);
        }

        [Fact]
        public void ObterNumerosJaUtilizadosNaGrade_DeveAdicionarNumerosDasColunasAEsquerda_QuandoColunaAtualMaiorQueZero()
        {
            // Arrange
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = 2,
                Grade = new int[2, 2] { { 0, 4 }, { 0, 0 } }
            };
            var numerosQuadradoAtual = new List<int> { 2 };
            int linhaAtual = 0;
            int colunaAtual = 1;

            // Act
            var resultado = SudokuIterator.ObterNumerosJaUtilizadosNaGrade(sudoku, numerosQuadradoAtual, linhaAtual, colunaAtual);

            // Assert
            Assert.Contains(2, resultado);
            Assert.Contains(0, resultado);
        }

        [Fact]
        public void ObterNumerosJaUtilizadosNaGrade_NaoDeveDuplicarNumeros()
        {
            // Arrange
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = 2,
                Grade = new int[2, 2] { { 5, 5 }, { 5, 5 } }
            };
            var numerosQuadradoAtual = new List<int> { 5 };
            int linhaAtual = 1;
            int colunaAtual = 1;

            // Act
            var resultado = SudokuIterator.ObterNumerosJaUtilizadosNaGrade(sudoku, numerosQuadradoAtual, linhaAtual, colunaAtual);

            // Assert
            Assert.Equal(1, resultado.FindAll(x => x == 5).Count);
        }
    }
}
