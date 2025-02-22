using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    public string tagName;
    public float maxDistance;
    public float maxAngle;
    public LayerMask layerMask = Physics.AllLayers;
    public abstract GameObject[] GetGameObjects();

    public bool CheckDirection(Vector3 direction)
    {
        var ray = new Ray(transform.position, transform.rotation * direction);
        return Physics.Raycast(ray, maxDistance, layerMask);
    }

    public virtual bool GetOpenDirection(ref Vector3 openDirection)
    {
        return false;
    }
}