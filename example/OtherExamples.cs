using Randomer;

public class OtherExamples
{
    private readonly Random _rnd;

    public OtherExamples(Random rnd)
    {
        _rnd = rnd;
    }

    public void RunAll()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n------- Other Extension Functions -------");
        Console.ResetColor();

        GenerateGuidExample();
        GenerateDateTimeExample();
        GenerateDateTimeWithTimeSpanExample();
    }

    private void GenerateGuidExample()
    {
        Console.WriteLine("\n=== GenerateGuid ===");
        Console.WriteLine($"Random GUID: {_rnd.GenerateGuid()}");
        Console.WriteLine($"Random GUID: {_rnd.GenerateGuid()}");
        Console.WriteLine($"Random GUID: {_rnd.GenerateGuid()}");
    }

    private void GenerateDateTimeExample()
    {
        Console.WriteLine("\n=== GenerateDateTime (range) ===");

        var startDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var endDate = new DateTimeOffset(2024, 12, 31, 23, 59, 59, TimeSpan.Zero);

        Console.WriteLine($"Random date between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}:");
        for (int i = 0; i < 3; i++)
        {
            var randomDate = _rnd.GenerateDateTime(startDate, endDate);
            Console.WriteLine($"  {randomDate:yyyy-MM-dd HH:mm:ss}");
        }
    }

    private void GenerateDateTimeWithTimeSpanExample()
    {
        Console.WriteLine("\n=== GenerateDateTime (TimeSpan) ===");

        var now = DateTimeOffset.UtcNow;
        var timeSpan = TimeSpan.FromDays(7);

        Console.WriteLine($"Random date within next 7 days from now:");
        for (int i = 0; i < 3; i++)
        {
            var randomDate = _rnd.GenerateDateTime(timeSpan);
            Console.WriteLine($"  {randomDate:yyyy-MM-dd HH:mm:ss}");
        }

        var pastTimeSpan = TimeSpan.FromDays(-30);
        Console.WriteLine($"\nRandom date within last 30 days:");
        for (int i = 0; i < 3; i++)
        {
            var randomDate = _rnd.GenerateDateTime(pastTimeSpan);
            Console.WriteLine($"  {randomDate:yyyy-MM-dd HH:mm:ss}");
        }
    }
}
