using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {

    [SerializeField] private Camera cam;
    [SerializeField] private float parallaxEffect;

    private float startPos, length;

    private void Start() {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    /// <summary>
    /// Updates the parallax effect in the FixedUpdate method.
    /// </summary>
    private void FixedUpdate() {
        // Calculate the distance to move the parallax layer based on the camera's position and parallax effect
        float dist = cam.transform.position.x * parallaxEffect;

        // Calculate the movement threshold based on the camera's position and the inverse of the parallax effect
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        // Update the transform position of the parallax layer
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        // Check if the movement exceeds the length of the parallax layer and adjust the start position accordingly
        if (movement > startPos + length) {
            // Move the start position to the right by the length of the parallax layer
            startPos += length;
        } else if (movement < startPos - length) {
            // Move the start position to the left by the length of the parallax layer
            startPos -= length;
        }
    }
}
