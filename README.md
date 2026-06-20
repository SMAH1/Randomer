# Randomer - Advanced Random Data Generation Library for C#

Randomer is a powerful and efficient library for C# developers that provides specialized functions for generating random data of various types. This library is built on Extension Methods that operate on the `Random` class.

## 🎯 Key Features

### 📋 Library Sections

#### 1. **Basic Extensions**
Core functions for generating simple data types:
- Generate random boolean values
- Generate random bytes
- Select random elements from arrays
- Generate non-repeating sets

**Example:**
```csharp
var rnd = new Random();

// Generate boolean with specific probability
bool result = rnd.GenerateBool(0.7f); // 70% chance of true

// Generate random byte
byte randomByte = rnd.GenerateByte();

// Select random element from array
int[] numbers = { 1, 2, 3, 4, 5 };
int random = rnd.GetOf(numbers);

// Select multiple elements without duplicates
int[] selected = rnd.GetOf(numbers, 3, allowDuplicates: false);
```

#### 2. **String Extensions**
Specialized functions for working with strings and specific formats:
- Select random character from string
- Generate random strings with specific characters
- Generate MAC addresses
- Generate IPv4 addresses

**Example:**
```csharp
var rnd = new Random();

// Select random character
char vowel = rnd.GetCharOf("aeiou");

// Generate random string with specific characters
char[] symbols = { 'a', 'b', 'c', '1', '2', '3' };
string randomStr = rnd.GenerateString(10, symbols);

// Generate random MAC address
string macAddress = rnd.GenerateMacAddress();
// Result: "A1:B2:C3:D4:E5:F6"

// Generate random IP address
string ipAddress = rnd.GenerateIPv4Address();
string ipWithConstraint = rnd.GenerateIPv4Address(192, 168); // Starting with 192.168
```

#### 3. **Math Extensions**
Probability distribution functions:
- Uniform Continuous Distribution
- Exponential Distribution
- Normal Distribution
- Uniform Discrete Distribution
- Bernoulli Distribution
- Binomial Distribution
- Poisson Distribution

**Example:**
```csharp
var rnd = new Random();

// Generate number with exponential distribution
double exponential = rnd.GeneratePDExponential(lambda: 0.2);
```

#### 4. **Other Extensions**
Specialized functions for other data types:
- Generate random GUID
- Generate random date and time

**Example:**
```csharp
var rnd = new Random();

// Generate GUID
Guid randomGuid = rnd.GenerateGuid();

// Generate date between two dates
var startDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
var endDate = new DateTimeOffset(2024, 12, 31, 23, 59, 59, TimeSpan.Zero);
DateTimeOffset randomDate = rnd.GenerateDateTime(startDate, endDate);

// Generate date in relative range (e.g., 30 days in the future)
DateTimeOffset futureDate = rnd.GenerateDateTime(TimeSpan.FromDays(30));
```

**Important Note about GUID Generation:**

While you can generate random GUIDs using the C# built-in library with `Guid.NewGuid()`, this library provides an alternative approach. If you need reproducible random data (same sequence of random values on every test run), you can create a `Random` instance with a fixed seed:

```csharp
// With a seeded Random, you get reproducible randomness
var seededRnd = new Random(seed: 12345);

// Every time you run with the same seed, you get the same GUID
Guid reproducibleGuid = seededRnd.GenerateGuid();
// Output: Always the same value (e.g., "a1b2c3d4-e5f6-4g7h-8i9j-0k1l2m3n4o5p")

// This is useful for testing scenarios where you want:
// - Randomized data to test your algorithms
// - But the same data on every test run for consistency and reproducibility
// - Allowing you to debug issues with predictable test data

// Example: Running tests multiple times with the same seed
var testRnd = new Random(42);
var testGuid1 = testRnd.GenerateGuid();
var testGuid2 = testRnd.GenerateGuid();

// Reset to the same seed for the next test run
testRnd = new Random(42);
var testGuid1_Again = testRnd.GenerateGuid();
var testGuid2_Again = testRnd.GenerateGuid();

// testGuid1 == testGuid1_Again ✓
// testGuid2 == testGuid2_Again ✓
```

## 🚀 Getting Started

### Adding the Library

```csharp
using Randomer;

var random = new Random();

// Use extension methods
char randomChar = random.GetCharOf("abcdefg");
string randomString = random.GenerateString(20, new[] { 'a', 'b', 'c' });
```

## 📁 Project Structure

```
Randomer/
├── src/                           # Library source code
│   ├── RandomExtensions.Basic.cs   # Basic extension functions
│   ├── RandomExtensions.String.cs  # String extension functions
│   ├── RandomExtensions.Math.cs    # Mathematical extension functions
│   ├── RandomExtensions.Other.cs   # Other extension functions
│   └── Randomer.csproj
├── example/                       # Code examples
│   ├── BasicExamples.cs
│   ├── StringExamples.cs
│   ├── MathExamples.cs
│   ├── OtherExamples.cs
│   └── Program.cs
├── test/                  # Unit tests
│   ├── BasicTest.cs
│   ├── StringTest.cs
│   ├── MathTest.cs
│   └── OtherTest.cs
└── README.md                      # This file
```

## 📚 More Examples

For more comprehensive and detailed examples, refer to the `example/` folder:

- **BasicExamples.cs**: Detailed examples of basic functions
- **StringExamples.cs**: Complete examples of string functions
- **MathExamples.cs**: Examples of mathematical functions
- **OtherExamples.cs**: Examples of other functions

To run the examples:

```bash
cd example
dotnet run
```

## ⚙️ Technical Requirements

- **.NET 8.0** or higher
- **C# 12** or higher
- Nullable Reference Types enabled
- AOT (Ahead-of-Time Compilation) compatible

## 🎓 Technologies Used

- **Partial Classes**: Logical organization of Extension methods
- **Extension Methods**: Adding functions without modifying the `Random` class
- **Span<T>**: Efficient memory usage
- **Stack Allocation**: Performance improvement for small buffers

## 📝 Notes

- All functions are thread-safe when using a single `Random` instance
- Comprehensive error handling for invalid inputs
- Optimized for high performance
- Compatible with AOT Compilation

## 📄 License

This project is licensed under the **MIT License**. You are free to use, modify, and distribute this library in your projects.

## 👨‍💻 Development

The project is actively maintained and regularly updated.

---

**Note**: For better understanding of each section, please review the code examples in the `example/` folder.
