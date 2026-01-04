using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Iterator
{
    public static class SudokuIterator
    {
        public static List<int> ObterNumerosJaUtilizadosNaGrade(Sudoku gradeSudoku, List<int> numerosUtilizadosNoQuadradoAtual,
            int linhaAtual, int colunaAtual)
        {
            var numerosJaUtilizados = new List<int>();
            numerosJaUtilizados.AddRange(numerosUtilizadosNoQuadradoAtual);

            if (linhaAtual != 0)
            {
                for (int linhaAcima = 0; linhaAcima < linhaAtual; linhaAcima++)
                {
                    if (!numerosJaUtilizados.Contains(gradeSudoku.Grade[linhaAcima, colunaAtual]))
                        numerosJaUtilizados.Add(gradeSudoku.Grade[linhaAcima, colunaAtual]);
                }
            }

            if (colunaAtual != 0)
            {
                for (int colunaAEsquerda = 0; colunaAEsquerda < colunaAtual; colunaAEsquerda++)
                {
                    if (!numerosJaUtilizados.Contains(gradeSudoku.Grade[linhaAtual, colunaAEsquerda]))
                        numerosJaUtilizados.Add(gradeSudoku.Grade[linhaAtual, colunaAEsquerda]);
                }
            }

            return numerosJaUtilizados;
        }

        public static List<int> ObterNumerosJaUtilizadosNoQuadrado(Sudoku gradeSudoku, BlocoDeQuadrado quadradoInternoSudoku)
        {
            var numerosJaUtilizados = new List<int>();
            for (int linha = quadradoInternoSudoku.InicioLinha; linha < quadradoInternoSudoku.FimLinha; linha++)
            {
                for (int coluna = quadradoInternoSudoku.InicioColuna; coluna < quadradoInternoSudoku.FimColuna; coluna++)
                {
                    if (!numerosJaUtilizados.Contains(gradeSudoku.Grade[linha, coluna]))
                        numerosJaUtilizados.Add(gradeSudoku.Grade[linha, coluna]);
                }
            }
            return numerosJaUtilizados;
        }
    }
}
