namespace Randomer;

public static partial class RandomExtensions
{
    public static char GetCharOf(this Random rnd, string str)
    {
        if (string.IsNullOrEmpty(str)) throw new ArgumentException(nameof(str));
        return str[rnd.Next(str.Length)];

    }

    /// <summary>
    /// Generates a random string of length 'length', each item is one char of 'signs'.
    /// </summary>
    /// <param name="length">A positive integer.</param>
    /// <param name="signs">List of character.</param>
    /// <returns>Random string of specify character.</returns>
    public static string GenerateString(this Random rnd, int length, char[] signs)
    {
        if (length <= 0 || signs == null || signs.Length < 2)
            throw new ArgumentException();

        Span<char> buffer = length <= 1024 ? stackalloc char[length] : new char[length];

        for (int i = 0; i < length; i++)
            buffer[i] = signs[rnd.Next(0, signs.Length)];

        return new string(buffer);
    }

    public static string GenerateMacAddress(this Random rnd)
    {
        Span<byte> buffer = stackalloc byte[6];

        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)rnd.Next(256);
        }

        return string.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
            buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5]);
    }

    public static string GenerateIPv4Address(this Random rnd, byte? first = null, byte? second = null, byte? third = null, byte? fourth = null)
    {
        byte octet1 = first ?? (byte)rnd.Next(256);
        byte octet2 = second ?? (byte)rnd.Next(256);
        byte octet3 = third ?? (byte)rnd.Next(256);
        byte octet4 = fourth ?? (byte)rnd.Next(256);

        return string.Format("{0}.{1}.{2}.{3}", octet1, octet2, octet3, octet4);
    }
}
