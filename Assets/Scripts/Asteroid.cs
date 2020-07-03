using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid spawned in game
/// </summary>
public class Asteroid : MonoBehaviour
{
    // Magnitude and direction of force
    float forceMagnitude;
    Vector2 forceDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        AddRandomForce();
    }

    // AddRandomForce adds a force with random mangitude and direction
    void AddRandomForce()
    {
        // Set random magnitude and direction for force
        forceMagnitude = Random.Range(25, 75);
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        forceDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Add force
        GetComponent<Rigidbody2D>().AddForce(forceMagnitude * forceDirection, ForceMode2D.Force);
    }
}
