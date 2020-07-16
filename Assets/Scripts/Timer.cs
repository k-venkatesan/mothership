using UnityEngine;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour
{
    #region Fields

    // Timer duration
    private float totalSeconds = 0;

    // Timer execution
    private float elapsedSeconds = 0;
    private bool running = false;

    // Support for Finished property
    private bool started = false;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Sets the duration of the timer
    /// The duration can only be set if the timer isn't currently running
    /// </summary>
    /// <value>duration</value>
    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }

    /// <summary>
    /// Gets whether or not the timer has finished running
    /// This property returns false if the timer has never been started
    /// </summary>
    /// <value>true if finished; otherwise, false.</value>
    public bool Finished => started && !running;

    /// <summary>
    /// Gets whether or not the timer is currently running
    /// </summary>
    /// <value>true if running; otherwise, false.</value>
    public bool Running => running;

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Runs the timer
    /// </summary>
    public void Run()
    {
        /* Timer only makes sense if the duration is greater than 0 - this
		 * also makes sure that the consumer actually sets the timer */
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;
        }
    }

    /// <summary>
    /// Updates timer and checks if it is finished
    /// </summary>
    private void UpdateTimerStatus()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
            }
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Update()
    {
        UpdateTimerStatus();
    }

    #endregion // MonoBehaviour Messages
}
