using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    #region Fields

    // Audio source
    private static AudioSource audioSource;

    // Dictionary that returns audio clip from Resources when AudioClipName is provided
    private static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">Audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.AsteroidHit, Resources.Load<AudioClip>("AsteroidHitClip"));
        audioClips.Add(AudioClipName.PlayerDeath, Resources.Load<AudioClip>("PlayerDeathClip"));
        audioClips.Add(AudioClipName.PlayerShot, Resources.Load<AudioClip>("PlayerShotClip"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">Name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    #endregion // Methods
}
