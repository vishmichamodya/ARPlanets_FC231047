using UnityEngine;

public class TouchGestures : MonoBehaviour {
    public float minScale = 0.3f;
    public float maxScale = 3f;

    void Update()
    {
        // ----- PC testing (XR Simulation) -----
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            float s = Mathf.Clamp(transform.localScale.x * (1f + scroll * 0.1f), minScale, maxScale);
            transform.localScale = Vector3.one * s;
        }
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(0f, 90f * Time.deltaTime, 0f, Space.World);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(0f, -90f * Time.deltaTime, 0f, Space.World);

        // ----- Phone: pinch and twist -----
        if (Input.touchCount != 2) return;

        Touch t0 = Input.GetTouch(0);
        Touch t1 = Input.GetTouch(1);

        Vector2 prev0 = t0.position - t0.deltaPosition;
        Vector2 prev1 = t1.position - t1.deltaPosition;

        float prevDistance = Vector2.Distance(prev0, prev1);
        float currentDistance = Vector2.Distance(t0.position, t1.position);
        if (prevDistance > 1f)
        {
            float factor = currentDistance / prevDistance;
            float newScale = Mathf.Clamp(transform.localScale.x * factor, minScale, maxScale);
            transform.localScale = Vector3.one * newScale;
        }

        float prevAngle = Mathf.Atan2(prev1.y - prev0.y, prev1.x - prev0.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.Atan2(t1.position.y - t0.position.y, t1.position.x - t0.position.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(prevAngle, currentAngle);
        transform.Rotate(0f, -deltaAngle, 0f, Space.World);
    }
}