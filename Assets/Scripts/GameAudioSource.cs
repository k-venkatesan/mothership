using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An audio source for the entire game
/// </summary>
public class GameAudioSource : MonoBehaviour
{
    #region Fields
    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes Audio Manager
    /// </summary>
    private void InitializeAudioManager()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Initialize(audioSource);
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    void Awake()
    {
        InitializeAudioManager();
    }

    #endregion // MonoBehaviour Messages
}
