using Randomer;

namespace RandomerTest;

public class MathTest
{
    // =====================================================================
    // Tests for GeneratePDExponential
    // =====================================================================

    [Fact]
    public void GenerateExponential_WithPositiveMean_ReturnsPositiveValue()
    {
        // Arrange
        var rnd = new Random(42);
        double mean = 5.0;

        // Act
        double result = rnd.GeneratePDExponential(mean);

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
        double result = rnd.GeneratePDExponential(mean);

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
            double result = rnd.GeneratePDExponential(mean);
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
        double value1 = rnd.GeneratePDExponential(mean);
        double value2 = rnd.GeneratePDExponential(mean);
        double value3 = rnd.GeneratePDExponential(mean);

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
        double result = rnd.GeneratePDExponential(mean);

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
        double result = rnd.GeneratePDExponential(mean);

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
        double lambda = 1.0 / expectedMean; // lambda is rate = 1/mean
        const int sampleCount = 10000;
        double tolerance = 0.2;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
        {
            sum += rnd.GeneratePDExponential(lambda);
        }
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to expected {expectedMean} (difference: {difference})");
    }

    // =====================================================================
    // Tests for GeneratePDUniformContinuous (no bounds) [0, 1)
    // =====================================================================

    [Fact]
    public void GenerateUniformContinuous_NoBounds_ReturnsValueInRange()
    {
        // Arrange
        var rng = new Random(42);

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            double result = RandomExtensions.GeneratePDUniformContinuous(rng);
            Assert.True(result >= 0.0 && result < 1.0,
                $"Value {result} should be in [0, 1)");
        }
    }

    [Fact]
    public void GenerateUniformContinuous_NoBounds_ReturnsFiniteValue()
    {
        // Arrange
        var rng = new Random(7);

        // Act
        double result = RandomExtensions.GeneratePDUniformContinuous(rng);

        // Assert
        Assert.True(double.IsFinite(result));
    }

    [Fact]
    public void GenerateUniformContinuous_NoBounds_MultipleCallsProduceDifferentValues()
    {
        // Arrange
        var rng = new Random(100);

        // Act
        double value1 = RandomExtensions.GeneratePDUniformContinuous(rng);
        double value2 = RandomExtensions.GeneratePDUniformContinuous(rng);
        double value3 = RandomExtensions.GeneratePDUniformContinuous(rng);

        // Assert
        Assert.NotEqual(value1, value2);
        Assert.NotEqual(value2, value3);
        Assert.NotEqual(value1, value3);
    }

    [Fact]
    public void GenerateUniformContinuous_NoBounds_With10000Samples_ConvergesToHalf()
    {
        // Arrange
        var rng = new Random();
        const int sampleCount = 10000;
        double expectedMean = 0.5;
        double tolerance = 0.02;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDUniformContinuous(rng);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to expected {expectedMean} (difference: {difference})");
    }

    // =====================================================================
    // Tests for GeneratePDUniformContinuous (with bounds) [a, b)
    // =====================================================================

    [Fact]
    public void GenerateUniformContinuous_WithBounds_ReturnsValueInRange()
    {
        // Arrange
        var rng = new Random(42);
        double a = 3.0;
        double b = 7.0;

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            double result = RandomExtensions.GeneratePDUniformContinuous(rng, a, b);
            Assert.True(result >= a && result < b,
                $"Value {result} should be in [{a}, {b})");
        }
    }

    [Theory]
    [InlineData(-10.0, -1.0)]
    [InlineData(0.0, 100.0)]
    [InlineData(-5.0, 5.0)]
    [InlineData(1000.0, 2000.0)]
    public void GenerateUniformContinuous_WithVariousBounds_ReturnsValueInRange(double a, double b)
    {
        // Arrange
        var rng = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            double result = RandomExtensions.GeneratePDUniformContinuous(rng, a, b);
            Assert.True(result >= a && result < b,
                $"Value {result} should be in [{a}, {b}) for a={a}, b={b}");
            Assert.True(double.IsFinite(result));
        }
    }

    [Fact]
    public void GenerateUniformContinuous_WithBounds_MultipleCallsProduceDifferentValues()
    {
        // Arrange
        var rng = new Random(55);
        double a = 10.0;
        double b = 20.0;

        // Act
        double value1 = RandomExtensions.GeneratePDUniformContinuous(rng, a, b);
        double value2 = RandomExtensions.GeneratePDUniformContinuous(rng, a, b);
        double value3 = RandomExtensions.GeneratePDUniformContinuous(rng, a, b);

        // Assert
        Assert.NotEqual(value1, value2);
        Assert.NotEqual(value2, value3);
        Assert.NotEqual(value1, value3);
    }

    [Fact]
    public void GenerateUniformContinuous_WithBounds_With10000Samples_ConvergesToMidpoint()
    {
        // Arrange
        var rng = new Random();
        double a = 2.0;
        double b = 8.0;
        double expectedMean = (a + b) / 2.0; // 5.0
        const int sampleCount = 10000;
        double tolerance = 0.1;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDUniformContinuous(rng, a, b);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to expected {expectedMean} (difference: {difference})");
    }

    // =====================================================================
    // Tests for GeneratePDNormal
    // =====================================================================

    [Fact]
    public void GenerateNormal_WithDefaults_ReturnsFiniteValue()
    {
        // Arrange
        var rng = new Random(42);

        // Act
        double result = RandomExtensions.GeneratePDNormal(rng);

        // Assert
        Assert.True(double.IsFinite(result));
    }

    [Fact]
    public void GenerateNormal_WithCustomMuAndSigma_ReturnsFiniteValue()
    {
        // Arrange
        var rng = new Random(42);
        double mu = 10.0;
        double sigma = 2.0;

        // Act
        double result = RandomExtensions.GeneratePDNormal(rng, mu, sigma);

        // Assert
        Assert.True(double.IsFinite(result));
    }

    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(5.0, 2.0)]
    [InlineData(-3.0, 0.5)]
    [InlineData(100.0, 10.0)]
    public void GenerateNormal_WithVariousParameters_ReturnsFiniteValues(double mu, double sigma)
    {
        // Arrange
        var rng = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            double result = RandomExtensions.GeneratePDNormal(rng, mu, sigma);
            Assert.True(double.IsFinite(result),
                $"All values should be finite for mu={mu}, sigma={sigma}");
        }
    }

    [Fact]
    public void GenerateNormal_MultipleCallsProduceDifferentValues()
    {
        // Arrange
        var rng = new Random(200);

        // Act
        double value1 = RandomExtensions.GeneratePDNormal(rng, 0, 1);
        double value2 = RandomExtensions.GeneratePDNormal(rng, 0, 1);
        double value3 = RandomExtensions.GeneratePDNormal(rng, 0, 1);

        // Assert
        Assert.NotEqual(value1, value2);
        Assert.NotEqual(value2, value3);
        Assert.NotEqual(value1, value3);
    }

    [Fact]
    public void GenerateNormal_With10000Samples_MeanConvergesToMu()
    {
        // Arrange
        var rng = new Random();
        double expectedMu = 7.0;
        double sigma = 2.0;
        const int sampleCount = 10000;
        double tolerance = 0.1;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDNormal(rng, expectedMu, sigma);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMu);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to expected mu={expectedMu} (difference: {difference})");
    }

    [Fact]
    public void GenerateNormal_WithDefaults_With10000Samples_MeanConvergesToZero()
    {
        // Arrange
        var rng = new Random();
        const int sampleCount = 10000;
        double tolerance = 0.1;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDNormal(rng);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to 0 (difference: {difference})");
    }

    [Fact]
    public void GenerateNormal_WithLargeSigma_ReturnsFiniteValues()
    {
        // Arrange
        var rng = new Random();
        double mu = 0.0;
        double sigma = 1000.0;

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            double result = RandomExtensions.GeneratePDNormal(rng, mu, sigma);
            Assert.True(double.IsFinite(result));
        }
    }

    // =====================================================================
    // Tests for GeneratePDUniformDiscrete
    // =====================================================================

    [Fact]
    public void GenerateUniformDiscrete_ReturnsValueInRange()
    {
        // Arrange
        var rng = new Random(42);
        int a = 1;
        int b = 10;

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            int result = RandomExtensions.GeneratePDUniformDiscrete(rng, a, b);
            Assert.InRange(result, a, b);
        }
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-5, 5)]
    [InlineData(1, 100)]
    [InlineData(50, 50)]
    public void GenerateUniformDiscrete_WithVariousRanges_ReturnsValueInRange(int a, int b)
    {
        // Arrange
        var rng = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int result = RandomExtensions.GeneratePDUniformDiscrete(rng, a, b);
            Assert.InRange(result, a, b);
        }
    }

    [Fact]
    public void GenerateUniformDiscrete_WithSameBounds_ReturnsOnlyThatValue()
    {
        // Arrange
        var rng = new Random();
        int value = 42;

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int result = RandomExtensions.GeneratePDUniformDiscrete(rng, value, value);
            Assert.Equal(value, result);
        }
    }

    [Fact]
    public void GenerateUniformDiscrete_MultipleCallsProduceDifferentValues()
    {
        // Arrange
        var rng = new Random(300);
        int a = 1;
        int b = 1000;
        var results = new HashSet<int>();

        // Act
        for (int i = 0; i < 50; i++)
            results.Add(RandomExtensions.GeneratePDUniformDiscrete(rng, a, b));

        // Assert - with a large range, we should get many different values
        Assert.True(results.Count > 10,
            $"Expected more than 10 distinct values, got {results.Count}");
    }

    [Fact]
    public void GenerateUniformDiscrete_AllPossibleValuesEventuallyGenerated()
    {
        // Arrange
        var rng = new Random();
        int a = 1;
        int b = 6; // Simulating a dice roll

        // Act
        var results = new HashSet<int>();
        for (int i = 0; i < 10000; i++)
            results.Add(RandomExtensions.GeneratePDUniformDiscrete(rng, a, b));

        // Assert - all values from 1 to 6 should appear
        for (int v = a; v <= b; v++)
            Assert.Contains(v, results);
    }

    [Fact]
    public void GenerateUniformDiscrete_With10000Samples_ConvergesToMidpoint()
    {
        // Arrange
        var rng = new Random();
        int a = 1;
        int b = 9;
        double expectedMean = (a + b) / 2.0; // 5.0
        const int sampleCount = 10000;
        double tolerance = 0.1;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDUniformDiscrete(rng, a, b);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to expected {expectedMean} (difference: {difference})");
    }

    // =====================================================================
    // Tests for GeneratePDBernoulli
    // =====================================================================

    [Fact]
    public void GenerateBernoulli_ReturnsOnlyZeroOrOne()
    {
        // Arrange
        var rng = new Random(42);
        double p = 0.5;

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            int result = RandomExtensions.GeneratePDBernoulli(rng, p);
            Assert.True(result == 0 || result == 1,
                $"Bernoulli should only return 0 or 1, got {result}");
        }
    }

    [Theory]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(0.9)]
    [InlineData(0.25)]
    [InlineData(0.75)]
    public void GenerateBernoulli_WithVariousProbabilities_ReturnsOnlyZeroOrOne(double p)
    {
        // Arrange
        var rng = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int result = RandomExtensions.GeneratePDBernoulli(rng, p);
            Assert.True(result == 0 || result == 1,
                $"Bernoulli should only return 0 or 1, got {result} for p={p}");
        }
    }

    [Fact]
    public void GenerateBernoulli_WithHighProbability_ReturnsMostlyOne()
    {
        // Arrange
        var rng = new Random();
        double p = 0.95;
        const int iterations = 1000;
        int onesCount = 0;

        // Act
        for (int i = 0; i < iterations; i++)
            onesCount += RandomExtensions.GeneratePDBernoulli(rng, p);

        // Assert
        double successRate = (double)onesCount / iterations;
        Assert.True(successRate > 0.9,
            $"With p=0.95, success rate {successRate} should be above 0.90");
    }

    [Fact]
    public void GenerateBernoulli_WithLowProbability_ReturnsMostlyZero()
    {
        // Arrange
        var rng = new Random();
        double p = 0.05;
        const int iterations = 1000;
        int onesCount = 0;

        // Act
        for (int i = 0; i < iterations; i++)
            onesCount += RandomExtensions.GeneratePDBernoulli(rng, p);

        // Assert
        double successRate = (double)onesCount / iterations;
        Assert.True(successRate < 0.1,
            $"With p=0.05, success rate {successRate} should be below 0.10");
    }

    [Fact]
    public void GenerateBernoulli_With10000Samples_MeanConvergesToP()
    {
        // Arrange
        var rng = new Random();
        double expectedP = 0.7;
        const int sampleCount = 10000;
        double tolerance = 0.02;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDBernoulli(rng, expectedP);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedP);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to p={expectedP} (difference: {difference})");
    }

    // =====================================================================
    // Tests for GeneratePDBinomial
    // =====================================================================

    [Fact]
    public void GenerateBinomial_ReturnsValueInRange()
    {
        // Arrange
        var rng = new Random(42);
        int n = 10;
        double p = 0.5;

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            int result = RandomExtensions.GeneratePDBinomial(rng, n, p);
            Assert.InRange(result, 0, n);
        }
    }

    [Theory]
    [InlineData(5, 0.3)]
    [InlineData(20, 0.5)]
    [InlineData(100, 0.1)]
    [InlineData(1, 0.8)]
    public void GenerateBinomial_WithVariousParameters_ReturnsValueInRange(int n, double p)
    {
        // Arrange
        var rng = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int result = RandomExtensions.GeneratePDBinomial(rng, n, p);
            Assert.InRange(result, 0, n);
        }
    }

    [Fact]
    public void GenerateBinomial_WithN1_ReturnsSameAsBernoulli()
    {
        // Arrange
        var rng = new Random(42);
        int n = 1;
        double p = 0.6;

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            int result = RandomExtensions.GeneratePDBinomial(rng, n, p);
            Assert.True(result == 0 || result == 1,
                $"Binomial(n=1, p) should only return 0 or 1, got {result}");
        }
    }

    [Fact]
    public void GenerateBinomial_MultipleCallsProduceDifferentValues()
    {
        // Arrange
        var rng = new Random(400);
        int n = 50;
        double p = 0.5;
        var results = new HashSet<int>();

        // Act
        for (int i = 0; i < 100; i++)
            results.Add(RandomExtensions.GeneratePDBinomial(rng, n, p));

        // Assert
        Assert.True(results.Count > 5,
            $"Expected multiple distinct values, got {results.Count}");
    }

    [Fact]
    public void GenerateBinomial_With10000Samples_MeanConvergesToNP()
    {
        // Arrange
        var rng = new Random();
        int n = 20;
        double p = 0.4;
        double expectedMean = n * p; // 8.0
        const int sampleCount = 10000;
        double tolerance = 0.2;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDBinomial(rng, n, p);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedMean);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to n*p={expectedMean} (difference: {difference})");
    }

    // =====================================================================
    // Tests for GeneratePDPoisson
    // =====================================================================

    [Fact]
    public void GeneratePoisson_ReturnsNonNegativeValue()
    {
        // Arrange
        var rng = new Random(42);
        double lambda = 3.0;

        // Act & Assert
        for (int i = 0; i < 1000; i++)
        {
            int result = RandomExtensions.GeneratePDPoisson(rng, lambda);
            Assert.True(result >= 0,
                $"Poisson distribution should return non-negative values, got {result}");
        }
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(5.0)]
    [InlineData(20.0)]
    public void GeneratePoisson_WithVariousLambdas_ReturnsNonNegativeValues(double lambda)
    {
        // Arrange
        var rng = new Random();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int result = RandomExtensions.GeneratePDPoisson(rng, lambda);
            Assert.True(result >= 0,
                $"All values should be non-negative for lambda={lambda}, got {result}");
        }
    }

    [Fact]
    public void GeneratePoisson_MultipleCallsProduceDifferentValues()
    {
        // Arrange
        var rng = new Random(500);
        double lambda = 10.0;
        var results = new HashSet<int>();

        // Act
        for (int i = 0; i < 100; i++)
            results.Add(RandomExtensions.GeneratePDPoisson(rng, lambda));

        // Assert
        Assert.True(results.Count > 5,
            $"Expected multiple distinct values, got {results.Count}");
    }

    [Fact]
    public void GeneratePoisson_With10000Samples_MeanConvergesToLambda()
    {
        // Arrange
        var rng = new Random();
        double expectedLambda = 4.0;
        const int sampleCount = 10000;
        double tolerance = 0.1;

        // Act
        double sum = 0;
        for (int i = 0; i < sampleCount; i++)
            sum += RandomExtensions.GeneratePDPoisson(rng, expectedLambda);
        double calculatedMean = sum / sampleCount;

        // Assert
        double difference = Math.Abs(calculatedMean - expectedLambda);
        Assert.True(difference <= tolerance,
            $"Mean of {calculatedMean} should be close to lambda={expectedLambda} (difference: {difference})");
    }

    [Fact]
    public void GeneratePoisson_WithSmallLambda_ReturnsSmallValues()
    {
        // Arrange
        var rng = new Random();
        double lambda = 0.1;
        const int iterations = 1000;

        // Act
        double sum = 0;
        for (int i = 0; i < iterations; i++)
            sum += RandomExtensions.GeneratePDPoisson(rng, lambda);
        double calculatedMean = sum / iterations;

        // Assert
        Assert.True(calculatedMean < 1.0,
            $"With small lambda={lambda}, mean {calculatedMean} should be less than 1");
    }

    [Fact]
    public void GeneratePoisson_WithLargeLambda_ReturnsValidValues()
    {
        // Arrange
        var rng = new Random();
        double lambda = 50.0;

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int result = RandomExtensions.GeneratePDPoisson(rng, lambda);
            Assert.True(result >= 0,
                $"Result should be non-negative for large lambda={lambda}");
        }
    }
}
