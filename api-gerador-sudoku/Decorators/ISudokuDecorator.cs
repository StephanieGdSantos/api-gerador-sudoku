using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Decorators
{
    public interface ISudokuDecorator
    {
        Sudoku CriarGradeDeSudokuJogavel(NivelEnum nivelDificuldade, int ordemGrade = 9);
    }
}