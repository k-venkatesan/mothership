using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid spawned in game
/// </summary>
public class Asteroid : MonoBehaviour
{
    #region Fields

    // Different asteroid sprites
    [SerializeField]
    private Sprite sprite1;
    [SerializeField]
    private Sprite sprite2;
    [SerializeField]
    private Sprite sprite3;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Is the asteroid large in size or not?
    /// </summary>
    private bool isLargeInSize
    {
        get { return transform.localScale[0] > 0.25; }
    }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Applies random sprite to asteroid
    /// </summary>
    private void ApplyRandomSprite()
    {
        // Apply sprite based on random number generated
        int choice = RandomGenerator.RandomNumberInRange(1, 3);
        switch (choice)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            default:
                Debug.LogWarning("Unexpected behaviour in asteroid sprite application");
                break;
        }
    }

    /// <summary>
    /// Checks if serialized fields have been filled in through drag-and-drop
    /// </summary>
    private void CheckIfSerializedFieldsPopulated()
    {
        if (sprite1 == null || sprite2 == null || sprite3 == null)
        {
            Debug.LogWarning("One or more prefab sprite fields not been filled-in. Please drag and drop into inspector window.");
        }
    }

    /// <summary>
    /// Checks if collider attached to given Collision2D object belong to a bullet
    /// </summary>
    /// <param name="collision"></param>
    /// <returns>'True' if collider belongs to bullet; 'False' if not</returns>
    private bool CollisionIsWithBullet(Collision2D collision)
    {
        /* Checking for existence of component is preferred over comparing
         * tags since string comparisons are prone to errors */
        return collision.gameObject.GetComponent<Bullet>() != null;
    }

    /// <summary>
    /// Initializes asteroid at given location and with motion in given (general) direction
    /// </summary>
    /// <param name="location">(x, y) location to initialize asteroid at</param>
    /// <param name="direction">General direction of asteroid motion</param>
    /// <param name="maxDeviationFromDirectionInDegrees">Maximum allowed deviation from given orthogonal direction (in degrees)</param>
    private void Initialize(Vector2 location, Direction direction, int maxDeviationFromDirectionInDegrees)
    {
        // Apply given location along with random rotation
        transform.position = location;
        transform.Rotate(new Vector3(0, 0, RandomGenerator.RandomNumberInRange(0, 360)));

        // Set random magnitude for force
        float forceMagnitude = RandomGenerator.RandomNumberInRange(25, 75);

        // Set angle of force within limited deviation from orthogonal direction
        float forceAngle;
        switch (direction)
        {
            case Direction.Left:
                forceAngle = RandomGenerator.RandomNumberInRange(180 - maxDeviationFromDirectionInDegrees, 180 + maxDeviationFromDirectionInDegrees) * Mathf.Deg2Rad;
                break;
            case Direction.Right:
                forceAngle = RandomGenerator.RandomNumberInRange(0 - maxDeviationFromDirectionInDegrees, 0 + maxDeviationFromDirectionInDegrees) * Mathf.Deg2Rad;
                break;
            case Direction.Up:
                forceAngle = RandomGenerator.RandomNumberInRange(90 - maxDeviationFromDirectionInDegrees, 90 + maxDeviationFromDirectionInDegrees) * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                forceAngle = RandomGenerator.RandomNumberInRange(270 - maxDeviationFromDirectionInDegrees, 270 + maxDeviationFromDirectionInDegrees) * Mathf.Deg2Rad;
                break;
            default:
                Debug.LogWarning("Unexpected behaviour in asteroid initialization");
                forceAngle = 0;
                break;
        }

        // Set direction for force based on angle
        Vector2 forceDirection = new Vector2(Mathf.Cos(forceAngle), Mathf.Sin(forceAngle));

        // Apply force
        GetComponent<Rigidbody2D>().AddForce(forceMagnitude * forceDirection, ForceMode2D.Force);
    }

    /// <summary>
    /// Initializes asteroid at given location and with motion in given (general) direction
    /// </summary>
    /// <param name="location">(x, y) location to initialize asteroid at</param>
    /// <param name="direction">General direction of asteroid motion</param>
    public void Initialize(Vector2 location, Direction direction)
    {
        // Set limits of deviation from orthogonal direction and initialize asteroid
        const int MaxDeviationFromDirectionInDegrees = 15;
        Initialize(location, direction, MaxDeviationFromDirectionInDegrees);
    }

    /// <summary>
    /// Initializes asteroid at given location and with motion in random direction
    /// </summary>
    /// <param name="location">(x, y) location to initialize asteroid at</param>
    private void Initialize(Vector2 location)
    {
        /* Deviation of +/- 180 degrees from 'Up' means that all
         * directions in space are possible - this will hold true even if
         * 'Up' is replaced by 'Left', 'Right' or 'Down' */
        Initialize(location, Direction.Up, 180);
    }

    /// <summary>
    /// Splits asteroid into two pieces with separate directions of motion
    /// </summary>
    private void SplitIntoTwo()
    {
        // Make size of asteroid and collider half of original
        transform.localScale /= 2;
        GetComponent<CircleCollider2D>().radius /= 2;

        // Spawn two instances of halved asteroid and initialize to provide random velocities
        Instantiate(gameObject).GetComponent<Asteroid>().Initialize(transform.position);
        Instantiate(gameObject).GetComponent<Asteroid>().Initialize(transform.position);

        // Destroy original so that only the two new instances remain
        Destroy(gameObject);
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        CheckIfSerializedFieldsPopulated();
    }

    private void Start()
    {
        ApplyRandomSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision is with bullet before splitting or destroying asteroid
        if (CollisionIsWithBullet(collision))
        {
            // Split asteroid if large in size; Destroy if small
            if (isLargeInSize)
            {
                SplitIntoTwo();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion // MonoBehaviour Messages

}
