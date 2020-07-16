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
    /// Destroys asteroid if colliding object is a bullet
    /// </summary>
    /// <param name="collision">Collision2D object containing information about collision</param>
    private void DestroyIfBullet(Collision2D collision)
    {
        /* Checking for existence of component is preferred over comparing
         * tags since string comparisons are prone to errors */
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes asteroid at given location and with motion in given (general) direction
    /// </summary>
    /// <param name="location">(x, y) location to initialize asteroid at</param>
    /// <param name="direction">General direction of asteroid motion</param>
    public void Initialize(Vector2 location, Direction direction)
    {
        // Apply given location along with random rotation
        transform.position = location;
        transform.Rotate(new Vector3(0, 0, RandomGenerator.RandomNumberInRange(0, 360)));

        // Set random magnitude for force
        float forceMagnitude = RandomGenerator.RandomNumberInRange(25, 75);

        // Set angle of force within limited deviation from orthogonal direction
        const int MaxDeviationInDegrees = 15;
        float forceAngle;
        switch (direction)
        {
            case Direction.Left:
                forceAngle = RandomGenerator.RandomNumberInRange(180 - MaxDeviationInDegrees, 180 + MaxDeviationInDegrees) * Mathf.Deg2Rad;
                break;
            case Direction.Right:
                forceAngle = RandomGenerator.RandomNumberInRange(0 - MaxDeviationInDegrees, 0 + MaxDeviationInDegrees) * Mathf.Deg2Rad;
                break;
            case Direction.Up:
                forceAngle = RandomGenerator.RandomNumberInRange(90 - MaxDeviationInDegrees, 90 + MaxDeviationInDegrees) * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                forceAngle = RandomGenerator.RandomNumberInRange(270 - MaxDeviationInDegrees, 270 + MaxDeviationInDegrees) * Mathf.Deg2Rad;
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
        DestroyIfBullet(collision);
    }

    #endregion // MonoBehaviour Messages

}
