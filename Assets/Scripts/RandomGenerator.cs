using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides global random number generator for one-time initialization
/// </summary>
public static class RandomGenerator
{
    #region Fields

    // Random number generator
    static System.Random random;

    #endregion

    #region Public Methods

    /// <summary>
    /// Initializes the random number generator
    /// </summary>
    public static void Initialize()
    {
        random = new System.Random();
    }

    /// <summary>
    /// Returns random integer with given lower and upper bounds (both inclusive)
    /// </summary>
    /// <param name="lowerBound">Lower bound of range (inclusive)</param>
    /// <param name="upperBound">Upper bound of range (inclusive)</param>
    /// <returns></returns>
    public static int RandomNumberInRange(int lowerBound, int upperBound)
    {
        return random.Next(lowerBound, upperBound + 1);
    }

    #endregion
}
