using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Timer bulletTimer;
    const float BulletLifeInSeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTimer();
    }

    // Update is called once per frame
    void Update()
    {
        MonitorBulletLife();
    }

    /// <summary>
    /// Applies impulse force to bullet in given direction
    /// </summary>
    /// <param name="forceDirection">Direction in which impulse force is to be applied</param>
    public void ApplyForce(Vector2 forceDirection)
    {
        const float LaunchForce = 10;
        GetComponent<Rigidbody2D>().AddForce(LaunchForce * forceDirection, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Sets timer up and runs it
    /// </summary>
    void InitializeTimer()
    {
        bulletTimer = GetComponent<Timer>();
        bulletTimer.Duration = BulletLifeInSeconds;
        bulletTimer.Run();
    }

    /// <summary>
    /// Checks timer and destroys bullet if time has elapsed
    /// </summary>
    void MonitorBulletLife()
    {
        if (bulletTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
