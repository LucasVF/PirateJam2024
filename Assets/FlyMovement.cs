using UnityEngine;

public class InfinitySymbolAnimation : MonoBehaviour
{
    public float speed = 1f;  // Speed of the animation
    public float scale = 1f;  // Scale of the infinity symbol

    private float time;
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Update the time variable based on speed
        time += Time.deltaTime * speed;

        // Calculate the position using the parametric equations for an infinity symbol
        float x = Mathf.Sin(time) * scale;
        float y = Mathf.Sin(2 * time) / 2 * scale;

        // Update the position of the GameObject relative to the initial position
        transform.localPosition = initialPosition + new Vector3(x, y, 0);
    }
}
