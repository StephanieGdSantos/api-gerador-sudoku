using APIGeradorSudoku.Entities.Options;
using APIGeradorSudoku.Providers;
using APIGeradorSudoku.Composites.Impl;
using APIGeradorSudoku.Entities;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIGeradorSudoku.UnitTests.Composites
{
    public class QuadradoCompositeImplTests
    {
        private readonly Mock<IRandomProvider> _randomProviderMock = new();
        private readonly Mock<IOptions<ConfiguracoesConstrucaoSudokuOptions>> _optionsMock = new();
        private readonly QuadradoCompositeImpl _quadradoCompositeImpl;

        public QuadradoCompositeImplTests()
        {
            _randomProviderMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);

            _optionsMock.Setup(o => o.Value).Returns(new ConfiguracoesConstrucaoSudokuOptions
            {
                NumeroMaximoTentativas = 2,
                NumerosPossiveisPorQuadrado = new int[] { 1, 2, 3, 4 },
                OrdemGradePadrao = 2
            });

            _quadradoCompositeImpl =  new QuadradoCompositeImpl(_randomProviderMock.Object, _optionsMock.Object);
        }

        [Fact]
        public void TentarPreencherQuadradoNVezes_DeveRetornarTrue_QuandoPreenchimentoBemSucedido()
        {
            // Arrange
            var sudoku = new Sudoku { OrdemGradeSudoku = 2, Grade = new int[2, 2] };
            var bloco = new BlocoDeQuadrado { InicioLinha = 0, FimLinha = 2, InicioColuna = 0, FimColuna = 2 };
            var numeroMaximoTentativas = 2;

            // Act
            var resultado = _quadradoCompositeImpl.TentarPreencherQuadradoNVezes(bloco, ref sudoku, numeroMaximoTentativas);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TentarPreencherQuadradoNVezes_DeveRetornarFalse_QuandoNaoConseguePreencher()
        {
            // Arrange
            var sudoku = new Sudoku { OrdemGradeSudoku = 2, Grade = new int[2, 2] };
            var bloco = new BlocoDeQuadrado { InicioLinha = 0, FimLinha = 2, InicioColuna = 0, FimColuna = 2 };
            var numeroMaximoTentativas = 0;

            // Act
            var resultado = _quadradoCompositeImpl.TentarPreencherQuadradoNVezes(bloco, ref sudoku, numeroMaximoTentativas);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void LimparQuadrado_DeveZerarTodosOsValoresDoQuadrado()
        {
            // Arrange
            var sudoku = new Sudoku { OrdemGradeSudoku = 2, Grade = new int[2, 2] { { 1, 2 }, { 3, 4 } } };
            var bloco = new BlocoDeQuadrado { InicioLinha = 0, FimLinha = 2, InicioColuna = 0, FimColuna = 2 };

            // Act
            _quadradoCompositeImpl.LimparQuadrado(bloco, ref sudoku);

            // Assert
            for (int i = bloco.InicioLinha; i < bloco.FimLinha; i++)
            {
                for (int j = bloco.InicioColuna; j < bloco.FimColuna; j++)
                {
                    Assert.Equal(0, sudoku.Grade[i, j]);
                }
            }
        }

        [Fact]
        public void PreencherQuadrado_DevePreencherComNumerosPossiveis()
        {
            // Arrange
            var sudoku = new Sudoku { OrdemGradeSudoku = 2, Grade = new int[2, 2] };
            var bloco = new BlocoDeQuadrado { InicioLinha = 0, FimLinha = 2, InicioColuna = 0, FimColuna = 2 };

            // Act
            var resultado = _quadradoCompositeImpl.PreencherQuadrado(bloco, ref sudoku);

            // Assert
            Assert.True(resultado);
            var possiveis = _optionsMock.Object.Value.NumerosPossiveisPorQuadrado;
            for (int i = bloco.InicioLinha; i < bloco.FimLinha; i++)
            {
                for (int j = bloco.InicioColuna; j < bloco.FimColuna; j++)
                {
                    Assert.Contains(sudoku.Grade[i, j], possiveis);
                }
            }
        }

        [Fact]
        public void PreencherQuadrado_DeveRetornarFalse_QuandoNaoConseguePreencher()
        {
            // Arrange
            _optionsMock.Setup(o => o.Value).Returns(new ConfiguracoesConstrucaoSudokuOptions
            {
                NumeroMaximoTentativas = 2,
                NumerosPossiveisPorQuadrado = new int[] { 1 },
                OrdemGradePadrao = 2
            });
            var quadradoComposite = new QuadradoCompositeImpl(_randomProviderMock.Object, _optionsMock.Object);

            var sudoku = new Sudoku { OrdemGradeSudoku = 2, Grade = new int[2, 2] { { 1, 1 }, { 1, 1 } } };
            var bloco = new BlocoDeQuadrado { InicioLinha = 0, FimLinha = 2, InicioColuna = 0, FimColuna = 2 };

            // Act
            var resultado = quadradoComposite.PreencherQuadrado(bloco, ref sudoku);

            // Assert
            Assert.False(resultado);
        }
    }
}
