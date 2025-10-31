namespace Randomer;

public static partial class RandomExtensions
{
    public static bool GenerateBool(this Random rnd, float percentOfTrue = 0.5F)
    {
        if (percentOfTrue <= 0 || percentOfTrue >= 1) throw new ArgumentException(nameof(percentOfTrue));

        return (rnd.Next(0, 1000000) / 1000000F) < percentOfTrue;
    }

    public static byte GenerateByte(this Random rnd)
    {
        return (byte)rnd.Next(0, 256);
    }

    public static T GetOf<T>(this Random rnd, T[] array)
    {
        return array[rnd.Next(array.Length)];
    }

    public static T[] GetOf<T>(this Random rnd, T[] array, int count, bool allowDuplicates = false)
    {
        if (count <= 1) throw new ArgumentException(nameof(count));
        if (!allowDuplicates && count > array.Length) throw new ArgumentException(nameof(count));

        T[] result = new T[count];
        if (allowDuplicates)
        {
            for (int i = 0; i < count; i++)
            {
                result[i] = array[rnd.Next(array.Length)];
            }
        }
        else
        {
            result = rnd.GenerateNonRepeatingSet(array.Length)
                .Select(index => array[index])
                .Take(count)
                .ToArray();
        }
        return result;
    }

    public static IEnumerable<int> GenerateNonRepeatingSet(this Random rnd, int count)
    {
        if (count <= 1)
            throw new ArgumentException();

        return Enumerable.Range(0, count)
                    .Select(i => new { rnd = rnd.Next(), data = i })
                    .OrderBy(x => x.rnd)
                    .Select(x => x.data)
                    .ToArray();
    }
}
