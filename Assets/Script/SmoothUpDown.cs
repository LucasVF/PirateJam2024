using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothUpDown : MonoBehaviour
{
    public float amplitude = 1.0f;  // The height of the wave
    public float frequency = 1.0f;  // The speed of the wave

    private Vector3 startPosition;

    void OnEnable()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
