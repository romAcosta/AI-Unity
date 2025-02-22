using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
    public override GameObject[] GetGameObjects()
    {
        var result = new List<GameObject>();

        var colliders = Physics.OverlapSphere(transform.position, maxDistance);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            if (tagName == "" || collider.tag == tagName)
            {
                // result.Add(collider.gameObject);
                var direction = collider.transform.position - transform.position;
                var angle = Vector3.Angle(direction, transform.forward);
                if (angle <= maxAngle) result.Add(collider.gameObject);
            }
        }

        return result.ToArray();
    }
}