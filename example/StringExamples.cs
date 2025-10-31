using Randomer;

public class StringExamples
{
    private readonly Random _rnd;

    public StringExamples(Random rnd)
    {
        _rnd = rnd;
    }

    public void RunAll()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------- String Extension Functions -------");
        Console.ResetColor();

        GetCharOfExample();
        GenerateStringExample();
        GenerateMacAddressExample();
        GenerateIPv4AddressExample();
    }

    private void GetCharOfExample()
    {
        Console.WriteLine("\n=== GetCharOf ===");

        string vowels = "aeiou";
        Console.WriteLine($"Random vowel: {_rnd.GetCharOf(vowels)}");
        Console.WriteLine($"Random vowel: {_rnd.GetCharOf(vowels)}");
        Console.WriteLine($"Random vowel: {_rnd.GetCharOf(vowels)}");
    }

    private void GenerateStringExample()
    {
        Console.WriteLine("\n=== GenerateString ===");

        char[] lowerLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        string randomString1 = _rnd.GenerateString(10, lowerLetters);
        Console.WriteLine($"Random string (10 chars, lowercase): {randomString1}");

        char[] numbers = "0123456789".ToCharArray();
        string randomString2 = _rnd.GenerateString(8, numbers);
        Console.WriteLine($"Random string (8 chars, digits): {randomString2}");

        char[] symbols = "!@#$%^&*()".ToCharArray();
        string randomString3 = _rnd.GenerateString(12, symbols);
        Console.WriteLine($"Random string (12 chars, symbols): {randomString3}");
    }

    private void GenerateMacAddressExample()
    {
        Console.WriteLine("\n=== GenerateMacAddress ===");
        Console.WriteLine($"Random MAC address: {_rnd.GenerateMacAddress()}");
        Console.WriteLine($"Random MAC address: {_rnd.GenerateMacAddress()}");
        Console.WriteLine($"Random MAC address: {_rnd.GenerateMacAddress()}");
    }

    private void GenerateIPv4AddressExample()
    {
        Console.WriteLine("\n=== GenerateIPv4Address ===");

        string ipFullRandom = _rnd.GenerateIPv4Address();
        Console.WriteLine($"Fully random IPv4: {ipFullRandom}");

        string ipPartial = _rnd.GenerateIPv4Address(192, 168);
        Console.WriteLine($"IPv4 with first two octets fixed (192.168.*.*): {ipPartial}");

        string ipSpecific = _rnd.GenerateIPv4Address(10, 0, 0);
        Console.WriteLine($"IPv4 with first three octets fixed (10.0.0.*): {ipSpecific}");
    }
}
