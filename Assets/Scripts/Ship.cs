using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Numerics;
using UnityEngine;

/// <summary>
/// Ship controlled by player
/// </summary>
public class Ship : MonoBehaviour
{
    #region Fields

    // Components
    Rigidbody2D rb2d;

    // Magnitude and direction of thrust
    const float ThrustForce = 10;
    float orientation;
    Vector2 thrustDirection;

    // Rotation speed
    const float RotateDegreesPerSecond = 60;

    // Radius of circle collider
    float radius;

    #endregion

    #region MonoBehaviour Methods

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

    // OnCollisionEnter2D is called when incoming collider makes contact
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy ship if incoming collider belongs to an asteroid
        if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Private Methods

    // ApplyThrust applies force in the direction that the ship is facing
    void ApplyThrust()
    {
        // Align thrust direction to ship orientation
        orientation = transform.eulerAngles[2] * Mathf.Deg2Rad;
        thrustDirection = new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation));

        // Apply thrust
        rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
    }

    // RotateShip makes ship rotate based on input
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

    #endregion
}
