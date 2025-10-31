using Randomer;

public class MathExamples
{
    private readonly Random _rnd;

    public MathExamples(Random rnd)
    {
        _rnd = rnd;
    }

    public void RunAll()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n------- Math Extension Functions -------");
        Console.ResetColor();

        GenerateExponentialExample();
    }

    private void GenerateExponentialExample()
    {
        Console.WriteLine("\n=== GenerateExponential ===");
        Console.WriteLine("Random exponential values with mean=2.0:");
        for (int i = 0; i < 5; i++)
        {
            double value = _rnd.GenerateExponential(2.0);
            Console.WriteLine($"  {value:F4}");
        }

        Console.WriteLine("\nRandom exponential values with mean=5.0:");
        for (int i = 0; i < 5; i++)
        {
            double value = _rnd.GenerateExponential(5.0);
            Console.WriteLine($"  {value:F4}");
        }
    }
}
