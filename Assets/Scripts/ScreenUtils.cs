using UnityEngine;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class ScreenUtils
{
    #region Fields

    // Screen dimensions saved to support resolution changes
    private static int screenWidth;
    private static int screenHeight;

    // Screen boundary coordinates cached for efficient boundary checking
    private static float screenLeft;
    private static float screenRight;
    private static float screenTop;
    private static float screenBottom;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Gets the left edge of the screen in world coordinates
    /// </summary>
    /// <value>Left edge of the screen</value>
    public static float ScreenLeft
    {
        get
        {
            CheckScreenSizeChanged();
            return screenLeft;
        }
    }

    /// <summary>
    /// Gets the right edge of the screen in world coordinates
    /// </summary>
    /// <value>Right edge of the screen</value>
    public static float ScreenRight
    {
        get
        {
            CheckScreenSizeChanged();
            return screenRight;
        }
    }

    /// <summary>
    /// Gets the top edge of the screen in world coordinates
    /// </summary>
    /// <value>Top edge of the screen</value>
    public static float ScreenTop
    {
        get
        {
            CheckScreenSizeChanged();
            return screenTop;
        }
    }

    /// <summary>
    /// Gets the bottom edge of the screen in world coordinates
    /// </summary>
    /// <value>Bottom edge of the screen</value>
    public static float ScreenBottom
    {
        get 
        {
            CheckScreenSizeChanged();
            return screenBottom; 
        }
    }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Checks for screen size change
    /// </summary>
    private static void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            Initialize();
        }
    }

    /// <summary>
    /// Initializes the screen utilities
    /// </summary>
    public static void Initialize()
    {
        // Save screen dimensions to support resolution changes
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }

    #endregion // Methods
}
