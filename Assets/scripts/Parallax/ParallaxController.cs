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

    private void FixedUpdate() {
        float dist = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (movement > startPos + length) {
            startPos += length;
        } else if (movement < startPos - length) {
            startPos -= length;
        }
    }
}
