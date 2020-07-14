using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    #region Private Fields

    // Radius of circle collider
    float radius;

    #endregion

    #region MonoBehaviour Methods

    // Start is called before the first frame update
    void Start()
    {
        GetColliderRadius();
    }

    // OnBecameInvisible is called when the game object is no longer visible
    void OnBecameInvisible()
    {
        WrapAroundScreen();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Stores collider radius for efficient retrieving
    /// </summary>
    void GetColliderRadius()
    {
        radius = GetComponent<CircleCollider2D>().radius;
    }

    /// <summary>
    /// Makes game object leaving one side of the screen appear on the opposite side
    /// </summary>
    void WrapAroundScreen()
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

    #endregion
}
