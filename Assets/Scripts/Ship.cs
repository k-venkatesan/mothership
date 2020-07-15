using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEditor.UIElements;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Timeline;

/// <summary>
/// The ship
/// </summary>
public class Ship : MonoBehaviour
{
    #region Serialized Fields

    // Bullet fired by ship
    [SerializeField]
    GameObject prefabBullet;

    #endregion

    #region Private Fields

    // Components 
    Rigidbody2D rb2d;

    // Magnitude and direction of forward thrust
    const float ThrustForce = 10;
    Vector2 thrustDirection;

    // Speed of rotation
    const float RotationDegreesPerSecond = 60;

    #endregion

    #region MonoBehaviour Methods

    // Awake is called before Start
    void Awake()
    {
        CheckIfSerializedFieldsPopulated();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeRigidbody2D();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotationInput();
        ProcessFiringInput();
    }

    // FixedUpdate is called with the frequency of the physics system
    void FixedUpdate()
    {
        ProcessThrustInput();        
    }

    // OnCollisionEnter2D is called when an incoming collider makes contact
    void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyIfAsteroid(collision);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Checks if serialized fields have been filled in through drag-and-drop
    /// </summary>
    void CheckIfSerializedFieldsPopulated()
    {
        if (prefabBullet == null)
        {
            Debug.LogWarning("Prefab Bullet has not been filled-in. Please drag and drop into inspector window.");
        }
    }

    /// <summary>
    /// Destroys ship if colliding object is an asteroid
    /// </summary>
    /// <param name="collision">Collision2D object containing information about collision</param>
    void DestroyIfAsteroid(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes Rigidbody2D component
    /// </summary>
    void InitializeRigidbody2D()
    {
        // Get reference to Rigidbody2D component for efficient force application
        rb2d = GetComponent<Rigidbody2D>();

        // Add drag so that ship does not stop suddenly when thrust is removed
        rb2d.drag = 2.5f;
    }

    /// <summary>
    /// Processes input and fires bullet if L-Ctrl key is pressed
    /// </summary>
    void ProcessFiringInput()
    {
        // GetKeyDown instead of GetAxis ensures that key will have to be released and pressed again to fire another bullet
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Spawn bullet from ship in direction it faces
            GameObject bullet = Instantiate(prefabBullet, transform.position, transform.rotation);

            // Apply impulse force to bullet
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }

    /// <summary>
    /// Processes input and rotates ship if 'Rotate' button is pressed
    /// </summary>
    void ProcessRotationInput()
    {
        // Get rotation input (in range [-1, 1])
        float rotationInput = Input.GetAxis("Rotate");

        // Rotate ship if non-zero rotation input is provided
        if (rotationInput != 0)
        {
            float rotationAmountWithSign = RotationDegreesPerSecond * Time.deltaTime * Math.Sign(rotationInput);
            transform.Rotate(new Vector3(0, 0, rotationAmountWithSign));
        }        
    }

    /// <summary>
    /// Processes input and applies thrust to ship if 'Thrust' button is pressed
    /// </summary>
    void ProcessThrustInput()
    {
        // Align thrust direction to ship orientation
        /* This is perfomed outside of the 'if' statement so that thrustDirection can be reused 
           by ProcessFiringInput even when 'Thrust' button has not been pressed after rotation */
        float shipOrientation = transform.eulerAngles[2] * Mathf.Deg2Rad;
        thrustDirection = new Vector2(Mathf.Cos(shipOrientation), Mathf.Sin(shipOrientation));

        // Apply thrust when button is pressed
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    #endregion
}
