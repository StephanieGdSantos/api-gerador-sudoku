using System.ComponentModel.DataAnnotations;

namespace APIGeradorSudoku.Entities.Options
{
    public class ConfiguracoesConstrucaoSudokuOptions
    {
        [Required]
        public int NumeroMaximoTentativas { get; set; }

        public int[]? NumerosPossiveisPorQuadrado { get; set; }

        [Required]
        public int OrdemGradePadrao { get; set; }
    }
}
