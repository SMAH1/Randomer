using Microsoft.Extensions.Time.Testing;
using Randomer;

namespace RandomerTest;

public class OtherTest
{
    private Random _random = new Random();

    // Tests for GenerateGuid
    [Fact]
    public void GenerateGuid_ReturnsValidGuid()
    {
        // Arrange & Act
        Guid result = _random.GenerateGuid();

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public void GenerateGuid_GeneratesDifferentGuids()
    {
        // Arrange & Act
        Guid guid1 = _random.GenerateGuid();
        Guid guid2 = _random.GenerateGuid();
        Guid guid3 = _random.GenerateGuid();

        // Assert
        Assert.NotEqual(guid1, guid2);
        Assert.NotEqual(guid2, guid3);
        Assert.NotEqual(guid1, guid3);
    }

    // Tests for GenerateDateTime with minTime and maxTime
    [Fact]
    public void GenerateDateTime_WithMinMaxTime_ReturnsDateTimeInRange()
    {
        // Arrange
        var minTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var maxTime = new DateTimeOffset(2025, 12, 31, 23, 59, 59, TimeSpan.Zero);

        // Act
        DateTimeOffset result = _random.GenerateDateTime(minTime, maxTime);

        // Assert
        Assert.InRange(result, minTime, maxTime);
    }

    [Fact]
    public void GenerateDateTime_WithMinMaxTime_ThrowsWhenMinGreaterThanMax()
    {
        // Arrange
        var minTime = new DateTimeOffset(2025, 12, 31, 23, 59, 59, TimeSpan.Zero);
        var maxTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateDateTime(minTime, maxTime));
    }

    [Fact]
    public void GenerateDateTime_WithMinMaxTime_ThrowsWhenMinEqualsMax()
    {
        // Arrange
        var minTime = new DateTimeOffset(2025, 6, 15, 12, 0, 0, TimeSpan.Zero);
        var maxTime = minTime;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateDateTime(minTime, maxTime));
    }

    [Fact]
    public void GenerateDateTime_WithMinMaxTime_GeneratesDifferentValues()
    {
        // Arrange
        var minTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var maxTime = new DateTimeOffset(2025, 12, 31, 23, 59, 59, TimeSpan.Zero);
        var results = new HashSet<DateTimeOffset>();

        // Act
        for (int i = 0; i < 10; i++)
        {
            results.Add(_random.GenerateDateTime(minTime, maxTime));
        }

        // Assert - Most values should be different (allowing for rare collision)
        Assert.True(results.Count > 1);
    }

    [Fact]
    public void GenerateDateTime_WithMinMaxTime_PreservesOffset()
    {
        // Arrange
        var offset = TimeSpan.FromHours(3);
        var minTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, offset);
        var maxTime = new DateTimeOffset(2025, 12, 31, 23, 59, 59, offset);

        // Act
        DateTimeOffset result = _random.GenerateDateTime(minTime, maxTime);

        // Assert
        Assert.Equal(offset, result.Offset);
    }

    // Tests for GenerateDateTime with TimeSpan
    [Fact]
    public void GenerateDateTime_WithPositiveTimeSpan_ReturnsDateTimeInRange()
    {
        // Arrange
        var timeSpan = TimeSpan.FromDays(30);

        // Act
        DateTimeOffset result = _random.GenerateDateTime(timeSpan);
        var now = TimeProvider.System.GetUtcNow();

        // Assert
        Assert.InRange(result, now, now.Add(timeSpan).AddSeconds(5)); // Adding 5 seconds buffer for timing
    }

    [Fact]
    public void GenerateDateTime_WithNegativeTimeSpan_ReturnsDateTimeInRange()
    {
        // Arrange
        var timeSpan = TimeSpan.FromDays(-30);
        var before = TimeProvider.System.GetUtcNow();

        // Act
        DateTimeOffset result = _random.GenerateDateTime(timeSpan);
        var after = TimeProvider.System.GetUtcNow();

        // Assert
        Assert.InRange(result, before.Add(timeSpan).AddSeconds(-5), after); // Adding -5 seconds buffer for timing
    }

    [Fact]
    public void GenerateDateTime_WithZeroTimeSpan_ReturnsCurrentTime()
    {
        // Arrange
        var timeSpan = TimeSpan.Zero;
        var before = TimeProvider.System.GetUtcNow();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateDateTime(timeSpan));
    }

    [Fact]
    public void GenerateDateTime_WithTimeSpan_UsesProvidedTimeProvider()
    {
        // Arrange
        var fixedTime = new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var timeProvider = new FakeTimeProvider(fixedTime);
        var timeSpan = TimeSpan.FromHours(1);

        // Act
        DateTimeOffset result = _random.GenerateDateTime(timeSpan, timeProvider);

        // Assert
        Assert.InRange(result, fixedTime, fixedTime.Add(timeSpan));
    }

    [Fact]
    public void GenerateDateTime_WithTimeSpan_DefaultsToSystemTimeProvider()
    {
        // Arrange
        var timeSpan = TimeSpan.FromMinutes(10);
        var before = TimeProvider.System.GetUtcNow();

        // Act
        DateTimeOffset result = _random.GenerateDateTime(timeSpan, null);
        var after = TimeProvider.System.GetUtcNow();

        // Assert
        Assert.InRange(result, before, after.Add(timeSpan).AddSeconds(5));
    }
}
