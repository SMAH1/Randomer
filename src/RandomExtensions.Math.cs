namespace Randomer;

public static partial class RandomExtensions
{
    public static double GenerateExponential(this Random rnd, double mean)
    {
        return -mean * Math.Log(1 - rnd.NextDouble());
    }
}
