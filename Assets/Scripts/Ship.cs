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
    const float ThrustForce = 10;
    float orientation;
    Vector2 thrustDirection;

    // Rotation speed
    const float RotateDegreesPerSecond = 50;

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

    // Update is called once per frame
    void Update()
    {
        // Rotate ship if 'Rotate' button is pressed
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0)
        {
            RotateShip(rotationInput);
        }
    }

    // FixedUpdate is called with the frequency of the physics system
    void FixedUpdate()
    {
        // Apply thrust if 'Thrust' button is pressed
        if (Input.GetAxis("Thrust") > 0)
        {
            ApplyThrust();
        }
    }

    // OnBecameInvisible is called when the game object is no longer visible by any camera
    void OnBecameInvisible()
    {
        WrapAroundScreen();
    }

    // WrapAroundScreen makes ship leaving one side of the screen appear on the opposite side
    void WrapAroundScreen()
    {
        // Position of ship
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

    // Rotate ship based on input
    void RotateShip(float rotationInput)
    {
        // Determine the amount of rotation to be applied (with appropriate sign)
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
        if (rotationInput < 0)
        {
            rotationAmount *= -1;
        }

        // Apply rotation
        transform.Rotate(new Vector3(0, 0, rotationAmount));
    }

    void ApplyThrust()
    {
        // Align thrust direction to ship orientation
        orientation = transform.eulerAngles[2] * Mathf.Deg2Rad;
        thrustDirection = new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation));

        // Apply thrust
        rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
    }

    #endregion
}
