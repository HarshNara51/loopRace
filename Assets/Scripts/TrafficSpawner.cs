using UnityEngine;
using UnityEngine.Splines;

public class TrafficSpawner : MonoBehaviour
{
    public GameObject[] trafficPrefabs;
    public SplineContainer[] lanes;

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
        if (lanes.Length == 0 || trafficPrefabs.Length == 0) return;

        // Pick random lane
        SplineContainer chosenLane = lanes[Random.Range(0, lanes.Length)];

        // Pick random car
        GameObject prefab = trafficPrefabs[Random.Range(0, trafficPrefabs.Length)];

        GameObject car = Instantiate(prefab);
        var follower = car.GetComponent<TrafficSplineFollower>();

        follower.spline = chosenLane;
        follower.speed = trafficSpeed;
    }
}