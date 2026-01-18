using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Iterator;

namespace APIGeradorSudoku.Solvers.Impl
{
    public class SudokuSolverImpl : ISudokuSolver
    {
        public int ContarSolucoes(int[,] grade)
        {
            int contador = 0;
            int limite = 2;
            Resolver(grade, ref contador, limite);
            return contador;
        }

        private bool Resolver(int[,] grade, ref int contador, int limite)
        {
            int tamanho = grade.GetLength(0);
            for (int linha = 0; linha < tamanho; linha++)
            {
                for (int coluna = 0; coluna < tamanho; coluna++)
                {
                    if (grade[linha, coluna] == 0)
                    {
                        for (int num = 1; num <= tamanho; num++)
                        {
                            if (PodeColocar(grade, linha, coluna, num))
                            {
                                grade[linha, coluna] = num;
                                if (Resolver(grade, ref contador, limite))
                                {
                                    grade[linha, coluna] = 0;
                                    return true;
                                }
                                grade[linha, coluna] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            contador++;
            return contador >= limite;
        }

        private bool PodeColocar(int[,] grade, int linha, int coluna, int num)
        {
            int tamanho = grade.GetLength(0);
            int ordemQuadrado = (int)Math.Sqrt(tamanho);

            for (int i = 0; i < tamanho; i++)
            {
                if (grade[linha, i] == num || grade[i, coluna] == num)
                    return false;
            }

            int inicioLinha = (linha / ordemQuadrado) * ordemQuadrado;
            int inicioColuna = (coluna / ordemQuadrado) * ordemQuadrado;
            for (int i = 0; i < ordemQuadrado; i++)
                for (int j = 0; j < ordemQuadrado; j++)
                    if (grade[inicioLinha + i, inicioColuna + j] == num)
                        return false;

            return true;
        }
    }
}
