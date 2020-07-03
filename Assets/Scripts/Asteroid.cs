using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid spawned in game
/// </summary>
public class Asteroid : MonoBehaviour
{
    // Available sprites
    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite3;

    // Magnitude and direction of force
    float forceMagnitude;
    Vector2 forceDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        ApplyRandomSprite();
        AddRandomForce();
    }

    // ApplyRandomSprite applies a sprite randomly
    void ApplyRandomSprite()
    {
        // Apply sprite based on random number generated
        int choice = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().sprite = choice switch
        {
            0 => sprite1,
            1 => sprite2,
            _ => sprite3,
        };
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
