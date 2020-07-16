/// <summary>
/// Provides global random number generator for one-time initialization
/// </summary>
public static class RandomGenerator
{
    #region Fields

    // Random number generator
    private static System.Random random;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes the random number generator
    /// </summary>
    public static void Initialize()
    {
        random = new System.Random();
    }

    /// <summary>
    /// Returns random integer within given lower and upper bounds (both inclusive)
    /// </summary>
    /// <param name="lowerBound">Lower bound of range (inclusive)</param>
    /// <param name="upperBound">Upper bound of range (inclusive)</param>
    /// <returns>Integer in range [lowerBound, upperBound]</returns>
    public static int RandomNumberInRange(int lowerBound, int upperBound)
    {
        return random.Next(lowerBound, upperBound + 1);
    }

    #endregion // Methods
}
