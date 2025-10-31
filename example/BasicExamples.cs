using NetEscapades.EnumGenerators;
using Randomer;

[EnumExtensions]
public enum PersonState
{
    Happy,
    Sad,
    Angry,
    Excited,
    Bored,
}

public class BasicExamples
{
    private readonly Random _rnd;

    public BasicExamples(Random rnd)
    {
        _rnd = rnd;
    }

    public void RunAll()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n------- Basic Extension Functions -------");
        Console.ResetColor();

        GenerateBoolExample();
        GenerateByteExample();
        GetOfSingleExample();
        GetOfSingleEnumExample();
        GetOfMultipleWithDuplicatesExample();
        GetOfMultipleWithoutDuplicatesExample();
        GenerateNonRepeatingSetExample();
    }

    private void GenerateBoolExample()
    {
        Console.WriteLine("\n=== GenerateBool ===");
        Console.WriteLine($"50% chance true: {_rnd.GenerateBool()}");
        Console.WriteLine($"70% chance true: {_rnd.GenerateBool(0.7F)}");
        Console.WriteLine($"20% chance true: {_rnd.GenerateBool(0.2F)}");
    }

    private void GenerateByteExample()
    {
        Console.WriteLine("\n=== GenerateByte ===");
        Console.WriteLine($"Random byte: {_rnd.GenerateByte()}");
        Console.WriteLine($"Random byte: {_rnd.GenerateByte()}");
        Console.WriteLine($"Random byte: {_rnd.GenerateByte()}");
    }

    private void GetOfSingleExample()
    {
        Console.WriteLine("\n=== GetOf (single element) ===");

        string[] colors = { "Red", "Green", "Blue", "Yellow", "Purple" };
        Console.WriteLine($"Random color: {_rnd.GetOf(colors)}");
        Console.WriteLine($"Random color: {_rnd.GetOf(colors)}");
        Console.WriteLine($"Random color: {_rnd.GetOf(colors)}");
    }

    private void GetOfSingleEnumExample()
    {
        Console.WriteLine("\n=== GetOf (single element of enum value that use source generator or reflection to get values) ===");

        Console.WriteLine($"Random enum value: {_rnd.GetOf(PersonStateExtensions.GetValues())}");
        Console.WriteLine($"Random enum value: {_rnd.GetOf(PersonStateExtensions.GetValues())}");
        Console.WriteLine($"Random enum value: {_rnd.GetOf(PersonStateExtensions.GetValues())}");
    }

    private void GetOfMultipleWithDuplicatesExample()
    {
        Console.WriteLine("\n=== GetOf (multiple with duplicates) ===");

        int[] numbers = { 10, 20, 30, 40, 50 };
        int[] selected = _rnd.GetOf(numbers, 5, allowDuplicates: true);
        Console.WriteLine($"5 random numbers (duplicates allowed): [{string.Join(", ", selected)}]");
    }

    private void GetOfMultipleWithoutDuplicatesExample()
    {
        Console.WriteLine("\n=== GetOf (multiple without duplicates) ===");

        string[] items = { "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape" };
        string[] unique = _rnd.GetOf(items, 4, allowDuplicates: false);
        Console.WriteLine($"4 unique items: [{string.Join(", ", unique)}]");
    }

    private void GenerateNonRepeatingSetExample()
    {
        Console.WriteLine("\n=== GenerateNonRepeatingSet ===");

        int[] shuffled = _rnd.GenerateNonRepeatingSet(6).ToArray();
        Console.WriteLine($"Shuffled indices (0-5): [{string.Join(", ", shuffled)}]");

        var cards = new[] { "A", "B", "C", "D", "E", "F" };
        var shuffledCards = shuffled.Select(i => cards[i]);
        Console.WriteLine($"Shuffled cards: [{string.Join(", ", shuffledCards)}]");
    }
}
