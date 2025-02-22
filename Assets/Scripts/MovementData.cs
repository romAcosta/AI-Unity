using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/MovementData")]
public class MovementData : ScriptableObject
{
    [Range(1, 20)] public float maxSpeed = 5f;
    [Range(1, 20)] public float minSpeed = 5f;
    [Range(1, 50)] public float maxForce = 3f;
    [Range(1, 50)] public float turnRate = 3f;
}