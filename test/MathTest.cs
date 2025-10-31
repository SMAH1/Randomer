using Randomer;

namespace RandomerTest;

public class MathTest
{
    [Fact]
    public void GenerateExponential_WithPositiveMean_ReturnsPositiveValue()
    {
        // Arrange
        var rnd = new Random(42);
        double mean = 5.0;

        // Act
        double result = rnd.GenerateExponential(mean);

        // Assert
        Assert.True(result >= 0, "Exponential distribution should return non-negative values");
    }

    [Fact]
    public void GenerateExponential_WithMeanOne_ReturnsValidValue()
    {
        // Arrange
        var rnd = new Random(123);
        double mean = 1.0;

        // Act
        double result = rnd.GenerateExponential(mean);

        // Assert
        Assert.True(result >= 0);
        Assert.True(double.IsFinite(result));
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    [InlineData(10.0)]
    public void GenerateExponential_WithVariousMeans_ReturnsPositiveValues(double mean)
    {
        // Arrange
        var rnd = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            double result = rnd.GenerateExponential(mean);
            Assert.True(result >= 0, $"All values should be non-negative for mean={mean}");
            Assert.True(double.IsFinite(result), $"All values should be finite for mean={mean}");
        }
    }

    [Fact]
    public void GenerateExponential_MultipleCallsWithSameSeed_ProducesDifferentValues()
    {
        // Arrange
        var rnd = new Random(999);
        double mean = 3.0;

        // Act
        double value1 = rnd.GenerateExponential(mean);
        double value2 = rnd.GenerateExponential(mean);
        double value3 = rnd.GenerateExponential(mean);

        // Assert
        Assert.NotEqual(value1, value2);
        Assert.NotEqual(value2, value3);
        Assert.NotEqual(value1, value3);
    }

    [Fact]
    public void GenerateExponential_WithLargeMean_ReturnsValidValue()
    {
        // Arrange
        var rnd = new Random();
        double mean = 1000.0;

        // Act
        double result = rnd.GenerateExponential(mean);

        // Assert
        Assert.True(result >= 0);
        Assert.True(double.IsFinite(result));
    }

    [Fact]
    public void GenerateExponential_WithSmallMean_ReturnsValidValue()
    {
        // Arrange
        var rnd = new Random();
        double mean = 0.01;

        // Act
        double result = rnd.GenerateExponential(mean);

        // Assert
        Assert.True(result >= 0);
        Assert.True(double.IsFinite(result));
    }

    [Fact]
    public void GenerateExponential_With10000Samples_ConvergesToMean()
    {
        // Arrange
        var rnd = new Random();
        double expectedMean = 5.0;
        const int sampleCount = 10000;
        double tolerance = 0.2; // 4% tolerance

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
        {
            sum += rnd.GenerateExponential(expectedMean);
        }
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to expected {expectedMean} (difference: {difference})");
    }
}