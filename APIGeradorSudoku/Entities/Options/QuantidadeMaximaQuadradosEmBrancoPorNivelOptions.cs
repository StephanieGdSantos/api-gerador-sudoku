using System.ComponentModel.DataAnnotations;

namespace APIGeradorSudoku.Entities.Options
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
