using UnityEngine;

/// <summary>
/// Screen wrapping is enabled for game objects that this script is attached to
/// (Screen wrapping makes an object leaving one side of the screen return from the other side)
/// </summary>
public class ScreenWrapper : MonoBehaviour
{
    #region Fields

    // Radius of circle collider
    private float radius;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Stores collider radius for efficient retrieving
    /// </summary>
    private void GetColliderRadius()
    {
        radius = GetComponent<CircleCollider2D>().radius;
    }

    /// <summary>
    /// Makes game object leaving one side of the screen appear on the opposite side
    /// </summary>
    private void WrapAroundScreen()
    {
        // Position of game object
        Vector2 position = transform.position;

        // Horizontal wrapping
        if (position.x > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft - radius;
            transform.position = position;
        }
        else if (position.x < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight + radius;
            transform.position = position;
        }

        // Vertical wrapping
        if (position.y > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom - radius;
            transform.position = position;
        }
        else if (position.y < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop + radius;
            transform.position = position;
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        GetColliderRadius();
    }

    private void OnBecameInvisible()
    {
        WrapAroundScreen();
    }

    #endregion // MonoBehaviour Messages
}
