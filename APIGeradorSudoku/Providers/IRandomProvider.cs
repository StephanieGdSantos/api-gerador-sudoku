namespace APIGeradorSudoku.Providers
{
    public interface IRandomProvider
    {
        int Next(int minValue, int maxValue);
        int Next(int maxValue);
    }

}
