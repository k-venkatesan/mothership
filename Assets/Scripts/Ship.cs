using System;
using UnityEngine;

/// <summary>
/// The ship
/// </summary>
public class Ship : MonoBehaviour
{
    #region Fields

    // Bullet to be fired by ship
    [SerializeField]
    private GameObject prefabBullet;

    // Components 
    private Rigidbody2D rb2d;

    // Magnitude and direction of forward thrust
    private const float ThrustForce = 10;
    private Vector2 thrustDirection;

    // Speed of rotation
    private const float RotationDegreesPerSecond = 60;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Checks if serialized fields have been filled in through drag-and-drop
    /// </summary>
    private void CheckIfSerializedFieldsPopulated()
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
    private void DestroyIfAsteroid(Collision2D collision)
    {
        /* Checking for existence of component is preferred over comparing
         * tags since string comparisons are prone to errors */
        if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes Rigidbody2D component
    /// </summary>
    private void InitializeRigidbody2D()
    {
        // Get reference to Rigidbody2D component for efficient force application
        rb2d = GetComponent<Rigidbody2D>();

        // Add drag so that ship does not stop suddenly when thrust is removed
        rb2d.drag = 2.5f;
    }

    /// <summary>
    /// Processes input and fires bullet if L-Ctrl key is pressed
    /// </summary>
    private void ProcessFiringInput()
    {
        /* GetKeyDown() is preferred over GetAxis() here since it returns true only in the first 
         * frame that the key is pressed and needs the key to be released and pressed again to 
         * return true once more - this as opposed to returning true for every frame that the key 
         * is held down for */
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Spawn bullet facing same direction as ship
            GameObject bullet = Instantiate(prefabBullet, transform.position, transform.rotation);

            // Apply impulse force to bullet
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }

    /// <summary>
    /// Processes input and rotates ship if 'Rotate' button is pressed
    /// </summary>
    private void ProcessRotationInput()
    {
        // Get rotation input (in range [-1, 1])
        float rotationInput = Input.GetAxis("Rotate");

        // Rotate ship if non-zero (either positive or negative) rotation input is provided
        if (rotationInput != 0)
        {
            float rotationAmountWithSign = RotationDegreesPerSecond * Time.deltaTime * Math.Sign(rotationInput);
            transform.Rotate(new Vector3(0, 0, rotationAmountWithSign));
        }
    }

    /// <summary>
    /// Processes input and applies thrust to ship if 'Thrust' button is pressed
    /// </summary>
    private void ProcessThrustInput()
    {
        /* Thrust direction is aligned to ship orientation outside of the 'if' statement so that 
         * the thrustDirection variable is kept updated even when there is no thrust input - this 
         * ensures that the correct direction is utilized by ProcessFiringInput() at all times */
        float shipOrientation = transform.eulerAngles[2] * Mathf.Deg2Rad;
        thrustDirection = new Vector2(Mathf.Cos(shipOrientation), Mathf.Sin(shipOrientation));

        // Apply thrust when button is pressed
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        CheckIfSerializedFieldsPopulated();
    }

    private void Start()
    {
        InitializeRigidbody2D();
    }

    private void Update()
    {
        ProcessRotationInput();
        ProcessFiringInput();
    }

    private void FixedUpdate()
    {
        ProcessThrustInput();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyIfAsteroid(collision);
    }

    #endregion // MonoBehaviour Messages
}
