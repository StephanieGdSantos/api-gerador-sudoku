using System.ComponentModel.DataAnnotations;

namespace api_gerador_sudoku.Options
{
    public class ConfiguracoesConstrucaoSudokuOptions
    {
        [Required]
        public int NumeroMaximoTentativas { get; set; }

        [Required]
        public int[] NumerosPossiveisPorQuadrado { get; set; }
    }
}
