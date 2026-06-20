using Randomer;
using System.Text.RegularExpressions;

namespace RandomerTest;

public class StringTest
{
    [Fact]
    public void GetCharOf_ReturnsCharFromString()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);
            var str = "hello";

            // Act
            var result = rnd.GetCharOf(str);

            // Assert
            Assert.Contains(result, str);
        }
    }

    [Fact]
    public void GetCharOf_WithSingleCharString()
    {
        // Arrange
        var rnd = new Random();
        var str = "a";

        // Act
        var result = rnd.GetCharOf(str);

        // Assert
        Assert.Equal('a', result);
    }

    [Fact]
    public void GetCharOf_WithNullString_ThrowsArgumentException()
    {
        // Arrange
        var rnd = new Random();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => rnd.GetCharOf(null!));
    }

    [Fact]
    public void GetCharOf_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        var rnd = new Random();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => rnd.GetCharOf(""));
    }

    [Fact]
    public void GenerateString_ReturnsCorrectLength()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);
            var signs = new[] { 'a', 'b', 'c', 'd' };
            var length = 100;

            // Act
            var result = rnd.GenerateString(length, signs);

            // Assert
            Assert.Equal(length, result.Length);
        }
    }

    [Fact]
    public void GenerateString_AllCharsFromSigns()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);
            var signs = new[] { 'x', 'y', 'z' };
            var length = 50;

            // Act
            var result = rnd.GenerateString(length, signs);

            // Assert
            Assert.All(result, c => Assert.Contains(c, signs));
        }
    }

    [Fact]
    public void GenerateString_LargeBuffer()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);
            var signs = new[] { '0', '1' };
            var length = 2000;

            // Act
            var result = rnd.GenerateString(length, signs);

            // Assert
            Assert.Equal(length, result.Length);
            Assert.All(result, c => Assert.Contains(c, signs));
        }
    }

    [Fact]
    public void GenerateString_WithZeroLength_ThrowsArgumentException()
    {
        // Arrange
        var rnd = new Random();
        var signs = new[] { 'a', 'b' };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => rnd.GenerateString(0, signs));
    }

    [Fact]
    public void GenerateString_WithNegativeLength_ThrowsArgumentException()
    {
        // Arrange
        var rnd = new Random();
        var signs = new[] { 'a', 'b' };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => rnd.GenerateString(-5, signs));
    }

    [Fact]
    public void GenerateString_WithNullSigns_ThrowsArgumentException()
    {
        // Arrange
        var rnd = new Random();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => rnd.GenerateString(10, null!));
    }

    [Fact]
    public void GenerateString_WithSingleSignChar_ThrowsArgumentException()
    {
        // Arrange
        var rnd = new Random();
        var signs = new[] { 'a' };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => rnd.GenerateString(10, signs));
    }

    [Fact]
    public void GenerateMacAddress_ReturnsValidFormat()
    {
        var regex = new Regex(@"^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$", RegexOptions.Compiled);
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);

            // Act
            var result = rnd.GenerateMacAddress();

            // Assert
            Assert.Matches(regex, result);
        }
    }

    [Fact]
    public void GenerateMacAddress_MultipleCallsDifferent()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random();

            // Act
            var result1 = rnd.GenerateMacAddress();
            var result2 = rnd.GenerateMacAddress();

            // Assert
            Assert.NotEqual(result1, result2);
        }
    }

    [Fact]
    public void GenerateMacAddress_HasCorrectLength()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);

            // Act
            var result = rnd.GenerateMacAddress();

            // Assert
            Assert.Equal(17, result.Length); // 6 pairs of hex chars + 5 colons
        }
    }

    [Fact]
    public void GenerateIPv4Address_ReturnsValidFormat()
    {
        var regex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$", RegexOptions.Compiled);
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);

            // Act
            var result = rnd.GenerateIPv4Address();

            // Assert
            Assert.Matches(regex, result);
        }
    }

    [Fact]
    public void GenerateIPv4Address_ValidIPRange()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);

            // Act
            var result = rnd.GenerateIPv4Address();

            // Assert
            var octets = result.Split('.');
            Assert.Equal(4, octets.Length);
            foreach (var octet in octets)
            {
                var num = byte.Parse(octet);
                Assert.InRange(num, 0, 255);
            }
        }
    }

    [Fact]
    public void GenerateIPv4Address_WithFirstOctet()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);

            // Act
            var result = rnd.GenerateIPv4Address(first: 192);

            // Assert
            Assert.StartsWith("192.", result);
        }
    }

    [Fact]
    public void GenerateIPv4Address_WithMultipleOctets()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random(42);

            // Act
            var result = rnd.GenerateIPv4Address(first: 192, second: 168, third: 1);

            // Assert
            Assert.StartsWith("192.168.1.", result);
        }
    }

    [Fact]
    public void GenerateIPv4Address_WithAllOctets()
    {
        // Arrange
        var rnd = new Random();

        // Act
        var result = rnd.GenerateIPv4Address(first: 10, second: 0, third: 0, fourth: 1);

        // Assert
        Assert.Equal("10.0.0.1", result);
    }

    [Fact]
    public void GenerateIPv4Address_MultipleCallsDifferent()
    {
        for (int i = 0; i < 1000; i++) // iterations
        {
            // Arrange
            var rnd = new Random();

            // Act
            var result1 = rnd.GenerateIPv4Address();
            var result2 = rnd.GenerateIPv4Address();

            // Assert
            Assert.NotEqual(result1, result2);
        }
    }
}