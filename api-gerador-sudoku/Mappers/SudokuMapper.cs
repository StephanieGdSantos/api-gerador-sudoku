using APIGeradorSudoku.DTOs;
using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Mappers
{
    public static class SudokuMapper
    {
        public static SudokuDTO ToDTO(Sudoku sudoku)
        {
            return new SudokuDTO
            {
                Grade = ConverterParaArrayDeArrays(sudoku.Grade),
                OrdemGradeSudoku = sudoku.OrdemGradeSudoku,
                OrdemQuadradoSudoku = sudoku.OrdemQuadradoSudoku
            };
        }

        public static int[][] ConverterParaArrayDeArrays(int[,] matriz)
        {
            int linhas = matriz.GetLength(0);
            int colunas = matriz.GetLength(1);
            int[][] resultado = new int[linhas][];
            for (int i = 0; i < linhas; i++)
            {
                resultado[i] = new int[colunas];
                for (int j = 0; j < colunas; j++)
                    resultado[i][j] = matriz[i, j];
            }
            return resultado;
        }
    }

}
