using APIGeradorSudoku.Composites;
using APIGeradorSudoku.Entities;

namespace APIGeradorSudoku.Buiders.Impl
{
    public class SudokuBuilderImpl(IQuadradoComposite quadradoComposite) : ISudokuBuilder
    {
        private readonly IQuadradoComposite _quadradoComposite = quadradoComposite;
        private readonly int _numeroMaximoTentativas = 3;

        public Sudoku CriarSudoku(int tamanhoGrade)
        {
            var sudoku = new Sudoku
            {
                OrdemGradeSudoku = tamanhoGrade,
                Grade = new int[tamanhoGrade, tamanhoGrade]
            };

            do
            {
                var quadradoBemSucedido = false;
                for (var inicioLinhaQuadrado = 0; inicioLinhaQuadrado < tamanhoGrade;
                    inicioLinhaQuadrado += sudoku.OrdemQuadradoSudoku)
                {
                    for (var inicioColunaQuadrado = 0; inicioColunaQuadrado < tamanhoGrade;
                        inicioColunaQuadrado += sudoku.OrdemQuadradoSudoku)
                    {
                        var blocoDeQuadradoAtual = new BlocoDeQuadrado
                        {
                            InicioLinha = inicioLinhaQuadrado,
                            FimLinha = inicioLinhaQuadrado + sudoku.OrdemQuadradoSudoku,
                            InicioColuna = inicioColunaQuadrado,
                            FimColuna = inicioColunaQuadrado + sudoku.OrdemQuadradoSudoku
                        };

                        quadradoBemSucedido = _quadradoComposite.TentarPreencherQuadradoNVezes(blocoDeQuadradoAtual,
                            ref sudoku, _numeroMaximoTentativas);

                        if (quadradoBemSucedido == false && inicioColunaQuadrado == 0)
                            break;

                        if (quadradoBemSucedido == false)
                            inicioColunaQuadrado -= sudoku.OrdemQuadradoSudoku * 2;
                    }

                    if (inicioLinhaQuadrado > 0 && quadradoBemSucedido == false)
                        inicioLinhaQuadrado -= sudoku.OrdemQuadradoSudoku * 2;
                }

            } while (sudoku.Grade[sudoku.OrdemQuadradoSudoku, sudoku.OrdemQuadradoSudoku] == 0);

            return sudoku;
        }
    }
}
