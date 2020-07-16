using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    #region Fields
    #endregion //Fields

    #region Properties
    #endregion // Properties

    #region Methods
    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        ScreenUtils.Initialize();
        RandomGenerator.Initialize();
    }

    #endregion // MonoBehaviour Messages
}
