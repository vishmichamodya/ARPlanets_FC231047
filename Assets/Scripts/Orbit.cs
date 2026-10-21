using UnityEngine;

public class Orbit : MonoBehaviour {
    public Transform center;          // what we go around (the Sun)
    public float orbitSpeed = 20f;    // degrees per second around the center
    public float spinSpeed = 50f;     // degrees per second around itself

    void Update()
    {
        if (center != null)
        {
            transform.RotateAround(center.position, center.up, orbitSpeed * Time.deltaTime);
        }
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
    }
}