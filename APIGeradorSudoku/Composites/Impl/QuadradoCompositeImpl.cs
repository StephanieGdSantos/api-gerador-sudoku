using APIGeradorSudoku.Entities.Options;
using APIGeradorSudoku.Providers;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Iterator;
using Microsoft.Extensions.Options;

namespace APIGeradorSudoku.Composites.Impl
{
    public class QuadradoCompositeImpl(IRandomProvider randomProvider,
        IOptions<ConfiguracoesConstrucaoSudokuOptions> 
        configuracoesConstrucaoSudokuOptions) : IQuadradoComposite
    {
        private readonly IRandomProvider _randomProvider = randomProvider;

        private readonly ConfiguracoesConstrucaoSudokuOptions 
            _configuracoesConstrucaoSudokuOptions = configuracoesConstrucaoSudokuOptions.Value;

        public void LimparQuadrado(BlocoDeQuadrado quadradoInternoSudoku, ref Sudoku gradeSudoku)
        {
            for (int linha = quadradoInternoSudoku.InicioLinha; linha < quadradoInternoSudoku.FimLinha; linha++)
            {
                for (int coluna = quadradoInternoSudoku.InicioColuna; coluna < quadradoInternoSudoku.FimColuna; coluna++)
                {
                    gradeSudoku.Grade[linha, coluna] = 0;
                }
            }
        }

        public bool PreencherQuadrado(BlocoDeQuadrado quadradoInternoSudoku, ref Sudoku gradeSudoku)
        {
            var numerosJaUtilizadosNoQuadrado = new List<int>();

            for (int linha = quadradoInternoSudoku.InicioLinha; linha < quadradoInternoSudoku.FimLinha; linha++)
            {
                for (int coluna = quadradoInternoSudoku.InicioColuna; coluna < quadradoInternoSudoku.FimColuna; coluna++)
                {
                    var numerosJaUtilizados = SudokuIterator.ObterNumerosJaUtilizadosNaGrade(gradeSudoku,
                        numerosJaUtilizadosNoQuadrado, linha, coluna);

                    var possibilidadeDeNumerosAtualmente = _configuracoesConstrucaoSudokuOptions
                        .NumerosPossiveisPorQuadrado
                        .Except(numerosJaUtilizados)
                        .ToArray();

                    if (possibilidadeDeNumerosAtualmente.Count() == 0)
                    {
                        return false;
                    }

                    var posicaoDeNumeroAleatorio = _randomProvider.Next(0, possibilidadeDeNumerosAtualmente.Length);

                    gradeSudoku.Grade[linha, coluna] = possibilidadeDeNumerosAtualmente[posicaoDeNumeroAleatorio];

                    numerosJaUtilizadosNoQuadrado.Add(possibilidadeDeNumerosAtualmente[posicaoDeNumeroAleatorio]);
                }
            }

            return true;
        }

        public bool TentarPreencherQuadradoNVezes(BlocoDeQuadrado quadradoInternoSudoku, 
            ref Sudoku gradeSudoku, int numeroMaximoTentativas)
        {
            var tentativas = 0;

            bool sucesso = false;
            while (sucesso == false && tentativas < numeroMaximoTentativas)
            {
                sucesso = PreencherQuadrado(quadradoInternoSudoku, ref gradeSudoku);

                if (sucesso == false)
                    LimparQuadrado(quadradoInternoSudoku, ref gradeSudoku);

                tentativas++;
            };

            return sucesso;
        }
    }
}
