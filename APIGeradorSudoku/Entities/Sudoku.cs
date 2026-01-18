namespace APIGeradorSudoku.Entities
{
    public class Sudoku
    {
        public int[,] Grade { get; set; }
        public int OrdemGradeSudoku { get; set; }
        public int OrdemQuadradoSudoku
        {
            get
            {
                return (int)Math.Sqrt(OrdemGradeSudoku);
            }
        }
    }
}
