using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Iterator;

namespace APIGeradorSudoku.Resolvers.Impl
{
    public class SudokuSolverImpl : ISudokuSolver
    {
        public int ContarSolucoes(int[,] grade)
        {
            int contador = 0;
            Resolver(grade, ref contador, 2);
            return contador;
        }

        private bool Resolver(int[,] grade, ref int contador, int limite)
        {
            for (int linha = 0; linha < 9; linha++)
            {
                for (int coluna = 0; coluna < 9; coluna++)
                {
                    if (grade[linha, coluna] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (PodeColocar(grade, linha, coluna, num))
                            {
                                grade[linha, coluna] = num;
                                if (Resolver(grade, ref contador, limite))
                                    return true;
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
            for (int i = 0; i < 9; i++)
            {
                if (grade[linha, i] == num || grade[i, coluna] == num)
                    return false;
            }
            int inicioLinha = linha / 3 * 3;
            int inicioColuna = coluna / 3 * 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grade[inicioLinha + i, inicioColuna + j] == num)
                        return false;
            return true;
        }
    }
}
