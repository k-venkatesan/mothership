using UnityEngine;

/// <summary>
/// Bullet fired by ship to destroy asteroids
/// </summary>
public class Bullet : MonoBehaviour
{
    #region Fields

    // Bullet lifetime definition
    private Timer bulletTimer;
    private const float BulletLifeInSeconds = 1;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Applies impulse force to bullet in given direction
    /// </summary>
    /// <param name="forceDirection">Direction in which impulse force is to be applied</param>
    public void ApplyForce(Vector2 forceDirection)
    {
        // Apply impulse force with set magnitude and given direction
        const float LaunchForce = 10;
        GetComponent<Rigidbody2D>().AddForce(LaunchForce * forceDirection, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Sets up timer and starts it
    /// </summary>
    private void InitializeTimer()
    {
        // Get reference to timer, set duration, start it
        bulletTimer = GetComponent<Timer>();
        bulletTimer.Duration = BulletLifeInSeconds;
        bulletTimer.Run();
    }

    /// <summary>
    /// Checks timer and destroys bullet if time has elapsed
    /// </summary>
    private void MonitorTimer()
    {
        if (bulletTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        InitializeTimer();
    }

    private void Update()
    {
        MonitorTimer();
    }

    #endregion // MonoBehaviour Messages
}
