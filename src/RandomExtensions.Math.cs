namespace Randomer;

public static partial class RandomExtensions
{
    /// <summary>
    /// Probability Distribution - Uniform Continuous [0, 1)
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <returns>A random number following a uniform continuous distribution between 0 and 1.</returns>
    public static double GeneratePDUniformContinuous(Random rng)
    {
        return rng.NextDouble();
    }

    /// <summary>
    /// Probability Distribution - Uniform Continuous  [a, b)
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="a">The lower bound of the uniform distribution.</param>
    /// <param name="b">The upper bound of the uniform distribution.</param>
    /// <returns>A random number following a uniform continuous distribution between a and b.</returns>
    public static double GeneratePDUniformContinuous(Random rng, double a, double b)
    {
        return a + (b - a) * rng.NextDouble();
    }

    /// <summary>
    /// Probability Distribution - Exponential
    /// </summary>
    /// <param name="rnd">The random number generator.</param>
    /// <param name="lambda">The rate parameter of the exponential distribution (Equal to 1/mean).</param>
    /// <returns>A random number following an exponential distribution with the specified rate parameter.</returns>
    public static double GeneratePDExponential(this Random rnd, double lambda)
    {
        double u = rnd.NextDouble();
        u = Math.Max(u, double.Epsilon); // Ensure u is not zero to avoid log(0)
        return -Math.Log(u) / lambda;
    }

    /// <summary>
    /// Probability Distribution - Normal (Gaussian) using Box-Muller transform
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="mu">The mean of the normal distribution.</param>
    /// <param name="sigma">The standard deviation of the normal distribution.</param>
    /// <returns>A random number following an following a normal distribution with the specified mean and standard deviation.</returns>
    public static double GeneratePDNormal(Random rng, double mu = 0, double sigma = 1)
    {
        double u1 = Math.Max(rng.NextDouble(), double.Epsilon);  // Ensure u1 is not zero to avoid log(0)
        double u2 = rng.NextDouble();
        double r = Math.Sqrt(-2 * Math.Log(u1));
        double theta = 2 * Math.PI * u2;
        double z = rng.NextDouble() < 0.5 ? r * Math.Cos(theta) : r * Math.Sin(theta);
        return mu + sigma * z;
    }

    /// <summary>
    /// Probability Distribution - Uniform Discrete [a, b]
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="a">The lower bound of the uniform discrete distribution.</param>
    /// <param name="b">The upper bound of the uniform discrete distribution.</param>
    /// <returns>A random integer following a uniform discrete distribution between a and b.</returns>
    public static int GeneratePDUniformDiscrete(Random rng, int a, int b)
    {
        return rng.Next(a, b + 1);
    }

    /// <summary>
    /// Probability Distribution - Bernoulli
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="p">The probability of success (1).</param>
    /// <returns>A random integer following a Bernoulli distribution with the specified probability of success.</returns>
    public static int GeneratePDBernoulli(Random rng, double p)
    {
        return rng.NextDouble() < p ? 1 : 0;
    }

    /// <summary>
    /// Probability Distribution - Binomial
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="n">The number of trials.</param>
    /// <param name="p">The probability of success in each trial.</param>
    /// <returns>A random integer following a binomial distribution with the specified parameters.</returns>
    public static int GeneratePDBinomial(Random rng, int n, double p)
    {
        int count = 0;
        for (int i = 0; i < n; i++)
            if (rng.NextDouble() < p) count++;
        return count;
    }

    /// <summary>
    /// Probability Distribution - Poisson
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="lambda">The rate parameter of the Poisson distribution (Equal to the mean).</param>
    /// <returns>A random integer following a Poisson distribution with the specified rate parameter.</returns>
    public static int GeneratePDPoisson(Random rng, double lambda)
    {
        double l = Math.Exp(-lambda);
        double p = 1.0;
        int k = 0;
        do
        {
            k++;
            p *= rng.NextDouble();
        } while (p > l);
        return k - 1;
    }
}
