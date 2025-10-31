namespace Randomer;

public static partial class RandomExtensions
{
    public static Guid GenerateGuid(this Random rnd)
    {
        Span<byte> buffer = stackalloc byte[16];

        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = rnd.GenerateByte();
        }

        return new Guid(buffer);
    }

    public static DateTimeOffset GenerateDateTime(this Random rnd, DateTimeOffset minTime, DateTimeOffset maxTime)
    {
        if (minTime >= maxTime)
            throw new ArgumentException("minTime must be less than maxTime");

        long ticksRange = maxTime.Ticks - minTime.Ticks;
        long randomTicks = (long)(rnd.NextDouble() * ticksRange);
        return new DateTimeOffset(minTime.Ticks + randomTicks, minTime.Offset);
    }

    public static DateTimeOffset GenerateDateTime(this Random rnd, TimeSpan timeSpan, TimeProvider? timeProvider = null)
    {
        if (timeSpan == TimeSpan.Zero)
            throw new ArgumentException("timeSpan must not be zero", nameof(timeSpan));

        timeProvider ??= TimeProvider.System;
        var now = timeProvider.GetUtcNow();

        DateTimeOffset minTime, maxTime;

        if (timeSpan >= TimeSpan.Zero)
        {
            minTime = now;
            maxTime = now.Add(timeSpan);
        }
        else
        {
            minTime = now.Add(timeSpan);
            maxTime = now;
        }

        return rnd.GenerateDateTime(minTime, maxTime);
    }
}
