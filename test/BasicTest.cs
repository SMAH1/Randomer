using Randomer;

namespace RandomerTest;

public class BasicTest
{
    private Random _random = new Random();

    // Tests for GenerateBool
    [Fact]
    public void GenerateBool_WithDefault_ReturnsBool()
    {
        // Arrange & Act
        bool result = _random.GenerateBool();

        // Assert
        Assert.IsType<bool>(result);
    }

    [Fact]
    public void GenerateBool_WithPercentOfTrue_ReturnsCorrectDistribution()
    {
        // Arrange
        float percentOfTrue = 0.8F;
        int iterations = 10000;
        int trueCount = 0;

        // Act
        for (int i = 0; i < iterations; i++)
        {
            if (_random.GenerateBool(percentOfTrue))
                trueCount++;
        }

        // Assert
        float actualPercentage = (float)trueCount / iterations;
        Assert.InRange(actualPercentage, 0.75F, 0.85F); // Allow 5% deviation
    }

    [Fact]
    public void GenerateBool_WithLowPercentage_ReturnsFalseMore()
    {
        // Arrange
        float percentOfTrue = 0.1F;
        int iterations = 1000;
        int trueCount = 0;

        // Act
        for (int i = 0; i < iterations; i++)
        {
            if (_random.GenerateBool(percentOfTrue))
                trueCount++;
        }

        // Assert
        Assert.True(trueCount < iterations / 2);
    }

    [Fact]
    public void GenerateBool_WithZeroPercentage_ThrowsException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateBool(0));
    }

    [Fact]
    public void GenerateBool_WithOneHundredPercentage_ThrowsException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateBool(1));
    }

    // Tests for GenerateByte
    [Fact]
    public void GenerateByte_ReturnsValidByte()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange & Act
            byte result = _random.GenerateByte();

            // Assert
            Assert.InRange(result, (byte)0, (byte)255);
        }
    }

    [Fact]
    public void GenerateByte_ReturnsMultipleDifferentValues()
    {
        // Arrange
        int iterations = 100;
        var results = new HashSet<byte>();

        // Act
        for (int i = 0; i < iterations; i++)
        {
            results.Add(_random.GenerateByte());
        }

        // Assert
        Assert.True(results.Count > 10); // Should get more than 10 different values
    }

    // Tests for GetOf<T> (single element)
    [Fact]
    public void GetOf_SingleElement_ReturnsElementFromArray()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5 };

            // Act
            int result = _random.GetOf(array);

            // Assert
            Assert.Contains(result, array);
        }
    }

    [Fact]
    public void GetOf_SingleElement_WithStrings_ReturnsValidString()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            string[] array = { "apple", "banana", "cherry" };

            // Act
            string result = _random.GetOf(array);

            // Assert
            Assert.Contains(result, array);
        }
    }

    [Fact]
    public void GetOf_SingleElement_WithSingleElement_ReturnsOnlyElement()
    {
        // Arrange
        int[] array = { 42 };

        // Act
        int result = _random.GetOf(array);

        // Assert
        Assert.Equal(42, result);
    }

    // Tests for GetOf<T> (multiple elements)
    [Fact]
    public void GetOf_MultipleElements_WithDuplicates_ReturnsCorrectCount()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5 };
            int count = 10;

            // Act
            int[] result = _random.GetOf(array, count, allowDuplicates: true);

            // Assert
            Assert.Equal(count, result.Length);
        }
    }

    [Fact]
    public void GetOf_MultipleElements_WithDuplicates_AllElementsFromArray()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5 };
            int count = 20;

            // Act
            int[] result = _random.GetOf(array, count, allowDuplicates: true);

            // Assert
            foreach (int element in result)
            {
                Assert.Contains(element, array);
            }
        }
    }

    [Fact]
    public void GetOf_MultipleElements_WithoutDuplicates_ReturnsCorrectCount()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5 };
            int count = 3;

            // Act
            int[] result = _random.GetOf(array, count, allowDuplicates: false);

            // Assert
            Assert.Equal(count, result.Length);
        }
    }

    [Fact]
    public void GetOf_MultipleElements_WithoutDuplicates_NoDuplicates()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int[] array = { 1, 2, 3, 4, 5 };
            int count = 3;

            // Act
            int[] result = _random.GetOf(array, count, allowDuplicates: false);

            // Assert
            Assert.Equal(count, result.Distinct().Count());
        }
    }

    [Fact]
    public void GetOf_MultipleElements_WithCountZero_ThrowsException()
    {
        // Arrange
        int[] array = { 1, 2, 3, 4, 5 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GetOf(array, 0, allowDuplicates: true));
    }

    [Fact]
    public void GetOf_MultipleElements_WithCountOne_ThrowsException()
    {
        // Arrange
        int[] array = { 1, 2, 3, 4, 5 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GetOf(array, 1, allowDuplicates: true));
    }

    [Fact]
    public void GetOf_MultipleElements_WithoutDuplicates_CountGreaterThanArrayLength_ThrowsException()
    {
        // Arrange
        int[] array = { 1, 2, 3 };
        int count = 5;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GetOf(array, count, allowDuplicates: false));
    }

    // Tests for GenerateNonRepeatingSet
    [Fact]
    public void GenerateNonRepeatingSet_ReturnsCorrectCount()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int count = 10;

            // Act
            var result = _random.GenerateNonRepeatingSet(count).ToArray();

            // Assert
            Assert.Equal(count, result.Length);
        }
    }

    [Fact]
    public void GenerateNonRepeatingSet_NoRepeatingValues()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int count = 100;

            // Act
            var result = _random.GenerateNonRepeatingSet(count).ToArray();

            // Assert
            Assert.Equal(count, result.Distinct().Count());
        }
    }

    [Fact]
    public void GenerateNonRepeatingSet_AllValuesInRange()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            int count = 50;

            // Act
            var result = _random.GenerateNonRepeatingSet(count).ToArray();

            // Assert
            Assert.All(result, value => Assert.InRange(value, 0, count - 1));
        }
    }

    [Fact]
    public void GenerateNonRepeatingSet_WithCount2_ReturnsCorrectly()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange & Act
            var result = _random.GenerateNonRepeatingSet(2).ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.NotEqual(result[0], result[1]);
        }
    }

    [Fact]
    public void GenerateNonRepeatingSet_WithCountOne_ThrowsException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateNonRepeatingSet(1).ToArray());
    }

    [Fact]
    public void GenerateNonRepeatingSet_WithCountZero_ThrowsException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _random.GenerateNonRepeatingSet(0).ToArray());
    }
}