using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [Header ("Parallax Background")]
    private GameObject camera;
    [SerializeField] private float parallaxEffect;
    private float xPosition;

    [Header ("Endless Background")]
    private float length;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;  // Get the width of the sprite
    }

    void Update()
    {
        float distanceMoved = camera.transform.position.x * (1-parallaxEffect); // Calculate the distance moved based on the camera's position and parallax effect
        float distanceToMove = camera.transform.position.x * parallaxEffect;    // Calculate the distance to move based on the camera's position and parallax effect
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y); // Update the position of the background
    
        if(distanceMoved > xPosition + length) // Check if the background has moved past the camera's position
        {
            xPosition += length; // Update the xPosition to the new position of the background
        }
        else if(distanceMoved < xPosition - length) // Check if the background has moved past the camera's position in the opposite direction
        {
            xPosition -= length; // Update the xPosition to the new position of the background
        }
    }
}
