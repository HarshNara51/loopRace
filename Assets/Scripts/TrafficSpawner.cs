using UnityEngine;
using UnityEngine.Splines;

public class TrafficSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TrafficLane
    {
        public SplineContainer spline;
        public bool reverseDirection;
    }

    public GameObject[] trafficPrefabs;
    public TrafficLane[] lanes;

    public float spawnInterval = 2f;
    public float trafficSpeed = 12f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnTraffic();
        }
    }

    void SpawnTraffic()
    {
        if (trafficPrefabs.Length == 0 || lanes.Length == 0) return;

        GameObject prefab =
            trafficPrefabs[Random.Range(0, trafficPrefabs.Length)];

        TrafficLane lane =
            lanes[Random.Range(0, lanes.Length)];

        GameObject car = Instantiate(prefab);
        TrafficSplineFollower follower =
            car.GetComponent<TrafficSplineFollower>();

        follower.spline = lane.spline;
        follower.reverseDirection = lane.reverseDirection;
        follower.speed = trafficSpeed;
    }
}