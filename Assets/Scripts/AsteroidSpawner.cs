using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    #region Fields

    [SerializeField]
    Asteroid prefabAsteroid;

    #endregion

    #region MonoBehaviour Methods

    // Start is called before the first frame update
    void Start()
    {
        // Spawn asteroid on each screen edge with motion towards opposite edge
        SpawnLeftToRight();
        SpawnRightToLeft();
        SpawnTopToBottom();
        SpawnBottomToTop();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Spawns asteroid at given screen edge and with given direction of motion
    /// </summary>
    /// <param name="directionOfSpawnLocationFromCentre">Direction that desired edge of screen lies in relation to centre</param>
    /// <param name="directionOfMotion">Direction of motion that asteroid is spawned with</param>
    void Spawn(Direction directionOfSpawnLocationFromCentre, Direction directionOfMotion)
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
            default:
                spawnLocation.x = 0;
                spawnLocation.y = ScreenUtils.ScreenBottom - asteroidRadius;
                break;
        }

        // Spawn asteroid
        asteroid.Initialize(spawnLocation, directionOfMotion);
    }

    /// <summary>
    /// Spawns asteroid just beyond bottom edge of screen with motion towards the left
    /// </summary>
    void SpawnBottomToTop()
    {
        Spawn(Direction.Down, Direction.Up);
    }

    /// <summary>
    /// Spawns asteroid just beyond left edge of screen with motion towards the right
    /// </summary>
    void SpawnLeftToRight()
    {
        Spawn(Direction.Left, Direction.Right);
    }

    /// <summary>
    /// Spawns asteroid just beyond right edge of screen with motion towards the left
    /// </summary>
    void SpawnRightToLeft()
    {
        Spawn(Direction.Right, Direction.Left);
    }

    /// <summary>
    /// Spawns asteroid just beyond top edge of screen with motion towards the bottom
    /// </summary>
    void SpawnTopToBottom()
    {
        Spawn(Direction.Up, Direction.Down);
    }    

    #endregion
}
