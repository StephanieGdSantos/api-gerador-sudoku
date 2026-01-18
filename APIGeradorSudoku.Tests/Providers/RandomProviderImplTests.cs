using APIGeradorSudoku.Providers.Impl;
using Xunit;

namespace APIGeradorSudoku.UnitTests.Providers
{
    public class RandomProviderImplTests
    {
        private readonly RandomProviderImp _randomProvider;

        public RandomProviderImplTests()
        {
            _randomProvider = new RandomProviderImp();
        }

        [Fact]
        public void Next_ComMinEMaxValue_DeveRetornarValorDentroDoIntervalo()
        {
            // Arrange
            int min = 1;
            int max = 10;
            bool dentroDoIntervalo = true;

            // Act
            for (int i = 0; i < 100; i++)
            {
                int valor = _randomProvider.Next(min, max);
                if (valor < min || valor >= max)
                {
                    dentroDoIntervalo = false;
                    break;
                }
            }

            // Assert
            Assert.True(dentroDoIntervalo);
        }

        [Fact]
        public void Next_ComMaxValue_DeveRetornarValorDentroDoIntervalo()
        {
            // Arrange
            int max = 5;
            bool dentroDoIntervalo = true;

            // Act
            for (int i = 0; i < 100; i++)
            {
                int valor = _randomProvider.Next(max);
                if (valor < 0 || valor >= max)
                {
                    dentroDoIntervalo = false;
                    break;
                }
            }

            // Assert
            Assert.True(dentroDoIntervalo);
        }
    }
}
