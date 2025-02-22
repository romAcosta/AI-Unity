using UnityEngine;
using Random = UnityEngine.Random;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint[] waypoints;


    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.TryGetComponent(out NavAgent Agent))
        //     Agent.waypoint = waypoints[Random.Range(0, waypoints.Length)];
    }
}