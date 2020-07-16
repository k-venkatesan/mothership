using UnityEngine;

/// <summary>
/// Screen wrapping is enabled for game objects that this script is attached to
/// (Screen wrapping makes an object leaving one side of the screen return from the other side)
/// </summary>
public class ScreenWrapper : MonoBehaviour
{
    #region Fields

    /* For game objects with box colliders - half-size is the half-width
     * For game objects with circle colliders - half-size is the radius */
    private float halfSize;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Stores collider half-size for efficient retrieving
    /// </summary>
    private void GetColliderHalfSize()
    {
        // Check collider type to determine half-size appropriately
        if (GetComponent<CircleCollider2D>() != null)
        {
            halfSize = GetComponent<CircleCollider2D>().radius;
        }
        else if (GetComponent<BoxCollider2D>() != null)
        {
            halfSize = GetComponent<BoxCollider2D>().size[0] / 2;
        }
        else
        {
            Debug.LogWarning("No collider found on object. Screen wrap will not function as expected.");
            halfSize = 0;
        }        
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
            position.x = ScreenUtils.ScreenLeft - halfSize;
            transform.position = position;
        }
        else if (position.x < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight + halfSize;
            transform.position = position;
        }

        // Vertical wrapping
        if (position.y > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom - halfSize;
            transform.position = position;
        }
        else if (position.y < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop + halfSize;
            transform.position = position;
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        GetColliderHalfSize();
    }

    private void OnBecameInvisible()
    {
        WrapAroundScreen();
    }

    #endregion // MonoBehaviour Messages
}
