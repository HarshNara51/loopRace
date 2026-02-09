using UnityEngine;
using UnityEngine.Splines;

public class TrafficSplineFollower : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 10f;
    private float distanceTravelled = 0f;

    void Update()
    {
        if (spline == null) return;

        distanceTravelled += speed * Time.deltaTime;
        float splineLength = spline.CalculateLength();
        float t = distanceTravelled / splineLength;

        if (t >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 position = spline.EvaluatePosition(t);
        Vector3 forward = spline.EvaluateTangent(t);

        transform.position = position;
        transform.rotation = Quaternion.LookRotation(forward);
    }
}