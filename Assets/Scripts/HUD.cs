using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Heads-Up Display (HUD) used to display information to player
/// </summary>
public class HUD : MonoBehaviour
{
    #region Fields

    // Text elements that display score and time
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timeText;

    // Prefixes to integer score and time displays
    const string scoreTextPrefix = "Score: ";
    const string timeTextPrefix = "Time: ";
    
    // Elapsed time in seconds
    float elapsedSeconds = 0;

    // Flag to check if the timer is to be kept running or not
    bool timerRunning = true;

    #endregion // Fields

    #region Components
    #endregion // Components

    #region Methods

    /// <summary>
    /// Checks if serialized fields have been filled in through drag-and-drop
    /// </summary>
    private void CheckIfSerializedFieldsPopulated()
    {
        // Check if score text object filled-in
        if (scoreText == null)
        {
            Debug.LogWarning("Game object for score text has not been filled-in. Please drag and drop into inspector window.");
        }

        // Check if timer text object filled-in
        if (timeText == null)
        {
            Debug.LogWarning("Game object for time text has not been filled-in. Please drag and drop into inspector window.");
        }
    }

    /// <summary>
    /// Set up score and time displays
    /// </summary>
    private void InitializeDisplays()
    {
        // Initialize score display
        scoreText.text = scoreTextPrefix + "0";

        // Initialize time display with integral part of seconds elapsed
        timeText.text = timeTextPrefix + ((int)elapsedSeconds).ToString();
    }

    /// <summary>
    /// Stops update of time in HUD
    /// </summary>
    public void StopTimer()
    {
        timerRunning = false;
    }

    /// <summary>
    /// Updates time when timer is running and displays new value in HUD
    /// </summary>
    private void UpdateRunningTimer()
    {
        if (timerRunning)
        {
            // Update time
            elapsedSeconds += Time.deltaTime;

            // Update time display with integral part of seconds elapsed
            timeText.text = timeTextPrefix + ((int)elapsedSeconds).ToString();
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
        InitializeDisplays();
    }

    void Update()
    {
        UpdateRunningTimer();
    }

    #endregion // MonoBehaviour Messages
}
