﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    #region Serialized Fields

    // Asteroid to be spawned
    [SerializeField]
    Asteroid prefabAsteroid;

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
        SpawnAsteroidLeftToRight();
        SpawnAsteroidRightToLeft();
        SpawnAsteroidTopToBottom();
        SpawnAsteroidBottomToTop();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Checks if serialized fields have been filled in through drag-and-drop
    /// </summary>
    void CheckIfSerializedFieldsPopulated()
    {
        if (prefabAsteroid == null)
        {
            Debug.LogWarning("Prefab Asteroid has not been filled-in. Please drag and drop into inspector window.");
        }
    }

    /// <summary>
    /// Spawns asteroid at given screen edge and with given direction of motion
    /// </summary>
    /// <param name="directionOfSpawnLocationFromCentre">Direction that desired edge of screen lies in relation to centre</param>
    /// <param name="directionOfMotion">Direction of motion that asteroid is spawned with</param>
    void SpawnAsteroid(Direction directionOfSpawnLocationFromCentre, Direction directionOfMotion)
    {
        // Instantiate asteroid
        Asteroid asteroid = Instantiate(prefabAsteroid);

        // Set spawn coordinates based on screen edge direction and asteroid radius
        float asteroidRadius = asteroid.GetComponent<CircleCollider2D>().radius;
        Vector2 spawnLocation = new Vector2();
        switch (directionOfSpawnLocationFromCentre)
        {
            case Direction.Left:
                spawnLocation.x = ScreenUtils.ScreenLeft - asteroidRadius;
                spawnLocation.y = 0;
                break;
            case Direction.Right:
                spawnLocation.x = ScreenUtils.ScreenRight + asteroidRadius;
                spawnLocation.y = 0;
                break;
            case Direction.Up:
                spawnLocation.x = 0;
                spawnLocation.y = ScreenUtils.ScreenTop + asteroidRadius;
                break;
            case Direction.Down:
                spawnLocation.x = 0;
                spawnLocation.y = ScreenUtils.ScreenBottom - asteroidRadius;
                break;
            default:
                Debug.LogWarning("Unexpected behaviour in asteroid spawning");
                spawnLocation.x = 0;
                spawnLocation.y = 0;
                break;
        }

        // Spawn asteroid
        asteroid.Initialize(spawnLocation, directionOfMotion);
    }

    /// <summary>
    /// Spawns asteroid just beyond bottom edge of screen with motion towards the top
    /// </summary>
    void SpawnAsteroidBottomToTop()
    {
        SpawnAsteroid(Direction.Down, Direction.Up);
    }

    /// <summary>
    /// Spawns asteroid just beyond left edge of screen with motion towards the right
    /// </summary>
    void SpawnAsteroidLeftToRight()
    {
        SpawnAsteroid(Direction.Left, Direction.Right);
    }

    /// <summary>
    /// Spawns asteroid just beyond right edge of screen with motion towards the left
    /// </summary>
    void SpawnAsteroidRightToLeft()
    {
        SpawnAsteroid(Direction.Right, Direction.Left);
    }

    /// <summary>
    /// Spawns asteroid just beyond top edge of screen with motion towards the bottom
    /// </summary>
    void SpawnAsteroidTopToBottom()
    {
        SpawnAsteroid(Direction.Up, Direction.Down);
    }    

    #endregion
}
