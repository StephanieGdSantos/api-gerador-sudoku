using System.ComponentModel.DataAnnotations;

namespace api_gerador_sudoku.Options
{
    public class QuantidadeMaximaQuadradosEmBrancoPorNivelOptions
    {
        [Required]
        public int Facil { get; set; }

        [Required]
        public int Medio { get; set; }

        [Required]
        public int Dificil { get; set; }
    }
}
