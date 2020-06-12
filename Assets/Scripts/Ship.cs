using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

/// <summary>
/// Ship in game
/// </summary>
public class Ship : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 thrustDirection;
    const float ThrustForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        thrustDirection = new Vector2(1, 0);

        // Provides damping when thrust stops being provided
        rb2d.drag = 2.5f;         
    }

    // FixedUpdate is called with the frequency of the physics system
    void FixedUpdate()
    {
        // Applies thrust when Thrust button is pressed
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }
}
