using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Numerics;
using UnityEngine;

/// <summary>
/// Ship in game
/// </summary>
public class Ship : MonoBehaviour
{
    #region Components

    Rigidbody2D rb2d;

    #endregion

    #region Fields

    // Magnitude and direction of thrust
    const float ThrustForce = 5;
    Vector2 thrustDirection = new Vector2(1,0);

    // Radius of circle collider
    float radius;

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        // Add drag so that ship does not stop suddenly when thrust is removed
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = 2.5f;

        // Get radius of circle collider
        radius = GetComponent<CircleCollider2D>().radius;
    }

    // FixedUpdate is called with the frequency of the physics system
    void FixedUpdate()
    {
        // Applies thrust when 'Thrust' button is pressed
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    // OnBecameInvisible is called when the game object is no longer visible by any camera
    void OnBecameInvisible()
    {
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
