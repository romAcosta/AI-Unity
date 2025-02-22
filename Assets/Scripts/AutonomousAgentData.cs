using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgentData", menuName = "Data/AutonomousAgentData")]
public class AutonomousAgentData : ScriptableObject
{
    [Header("Wander")] public float displacement;

    public float radius;
    public float distance;

    [Range(0, 5)] public float cohesionWeight;
    [Range(0, 5)] public float separationWeight;
    [Range(0, 5)] public float separationRadius;
    [Range(0, 5)] public float alignmentWeight;
    [Range(0, 20)] public float obstacleWeight;
}