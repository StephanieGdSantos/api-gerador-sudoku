using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Composites
{
    public interface IQuadradoComposite
    {
        bool TentarPreencherQuadradoNVezes(BlocoDeQuadrado blocoDeQuadradoSudoku, ref Sudoku gradeSudoku, 
            int numeroMaximoTentativas);
    }
}
