using UnityEngine;
using UnityEngine.Splines;

public class TrafficSplineFollower : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 10f;
    public bool reverseDirection = false;

    private float distanceTravelled = 0f;

    void Update()
    {
        if (spline == null) return;

        distanceTravelled += speed * Time.deltaTime;

        float splineLength = spline.CalculateLength();
        float t = distanceTravelled / splineLength;

        // Despawn when finished
        if (t >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        // Reverse direction if needed
        if (reverseDirection)
            t = 1f - t;

        Vector3 position = spline.EvaluatePosition(t);
        Vector3 tangent = spline.EvaluateTangent(t);

        // Flip forward direction if reversed
        if (reverseDirection)
            tangent = -tangent;

        transform.position = position;
        transform.rotation = Quaternion.LookRotation(tangent);
    }
}