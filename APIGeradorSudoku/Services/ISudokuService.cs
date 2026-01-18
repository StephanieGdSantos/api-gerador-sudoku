using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Services
{
    public interface ISudokuService
    {
        Sudoku CriarGradeDeSudokuJogavel(NivelEnum nivelDificuldade);
    }
}