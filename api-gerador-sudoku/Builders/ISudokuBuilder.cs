using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Buiders
{
    public interface ISudokuBuilder
    {
        Sudoku CriarSudoku(int tamanhoGrade);
    }
}
