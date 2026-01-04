using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Iterator;

namespace APIGeradorSudoku.Composites.Impl
{
    public class QuadradoCompositeImpl : IQuadradoComposite
    {
        private readonly int[] _numerosPossiveis = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly Random _random;

        public QuadradoCompositeImpl()
        {
            _random = new Random();
        }

        private void LimparQuadrado(BlocoDeQuadrado quadradoInternoSudoku, ref Sudoku gradeSudoku)
        {
            for (int linha = quadradoInternoSudoku.InicioLinha; linha < quadradoInternoSudoku.FimLinha; linha++)
            {
                for (int coluna = quadradoInternoSudoku.InicioColuna; coluna < quadradoInternoSudoku.FimColuna; coluna++)
                {
                    gradeSudoku.Grade[linha, coluna] = 0;
                }
            }
        }

        private bool PreencherQuadrado(BlocoDeQuadrado quadradoInternoSudoku, ref Sudoku gradeSudoku)
        {
            var numerosJaUtilizadosNoQuadrado = new List<int>();

            for (int linha = quadradoInternoSudoku.InicioLinha; linha < quadradoInternoSudoku.FimLinha; linha++)
            {
                for (int coluna = quadradoInternoSudoku.InicioColuna; coluna < quadradoInternoSudoku.FimColuna; coluna++)
                {
                    var numerosJaUtilizados = SudokuIterator.ObterNumerosJaUtilizadosNaGrade(gradeSudoku,
                        numerosJaUtilizadosNoQuadrado, linha, coluna);

                    var possibilidadeDeNumerosAtualmente = _numerosPossiveis.Except(numerosJaUtilizados).ToArray();

                    if (possibilidadeDeNumerosAtualmente.Count() == 0)
                    {
                        return false;
                    }

                    var posicaoDeNumeroAleatorio = _random.Next(0, possibilidadeDeNumerosAtualmente.Length);

                    gradeSudoku.Grade[linha, coluna] = possibilidadeDeNumerosAtualmente[posicaoDeNumeroAleatorio];

                    numerosJaUtilizadosNoQuadrado.Add(possibilidadeDeNumerosAtualmente[posicaoDeNumeroAleatorio]);
                }
            }

            return true;
        }

        public bool TentarPreencherQuadradoNVezes(BlocoDeQuadrado quadradoInternoSudoku, ref Sudoku gradeSudoku,
            int numeroMaximoTentativas)
        {
            var tentativas = 0;

            bool sucesso;
            do
            {
                sucesso = PreencherQuadrado(quadradoInternoSudoku, ref gradeSudoku);

                if (sucesso == false)
                    LimparQuadrado(quadradoInternoSudoku, ref gradeSudoku);

                tentativas++;
            } while (sucesso == false && tentativas < numeroMaximoTentativas);

            return sucesso;
        }
    }
}
