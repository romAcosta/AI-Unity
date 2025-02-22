using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


public class AutonomousAgent : AIAgent
{
    public AutonomousAgentData data;

    [Header("Perception")] public Perception seekPerception;

    public Perception fleePerception;
    public Perception flockPerception;
    public Perception obstaclePerception;

    private float angle;

    public void Update()
    {
        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
        Obstacle();
        Flee();
        Seek();
        Flock();
        Wander();
        if (movement.Acceleration.sqrMagnitude != 0)
            transform.rotation = Quaternion.LookRotation(movement.Direction, Vector3.up);
    }


    private Vector3 Seek(GameObject go)
    {
        var direction = go.transform.position - transform.position;
        var force = GetSteeringForce(direction);
        return force;
    }

    private Vector3 Flee(GameObject go)
    {
        var direction = transform.position - go.transform.position;
        var force = GetSteeringForce(direction);
        return force;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        var desired = direction.normalized * movement.data.maxSpeed;
        var steeringForce = desired - movement.Velocity;
        var force = Vector3.ClampMagnitude(steeringForce, movement.data.maxForce);

        return force;
    }

    private void Obstacle()
    {
        if (obstaclePerception != null && obstaclePerception.CheckDirection(Vector3.forward))
        {
            var direction = Vector3.zero;
            if (obstaclePerception.GetOpenDirection(ref direction))
            {
                Debug.DrawRay(transform.position, direction * 5, Color.red, 0.2f);
                movement.ApplyForce(GetSteeringForce(direction) * data.obstacleWeight);
            }
        }
    }

    private void Wander()
    {
        //WANDER
        if (movement.Acceleration.sqrMagnitude == 0)
        {
            angle += Random.Range(-data.displacement, data.displacement);
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            var point = rotation * (Vector3.forward * data.radius);
            var forward = movement.Direction * data.distance;
            var force = GetSteeringForce(point + forward);
            movement.ApplyForce(force);
        }
    }

    private void Flee()
    {
        //Flee
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                var force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
    }

    private void Seek()
    {
        //SEEK
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                var force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
    }

    private void Flock()
    {
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects) * data.cohesionWeight);
                movement.ApplyForce(Separation(gameObjects, data.separationRadius) * data.separationWeight);
                movement.ApplyForce(Alignment(gameObjects) * data.alignmentWeight);
            }
        }
    }

    private Vector3 Cohesion(GameObject[] neighbors)
    {
        var positions = Vector3.zero;
        foreach (var neighbor in neighbors) positions += neighbor.transform.position;

        var center = positions / neighbors.Length;
        var direction = center - transform.position;
        var force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        var separation = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            var direction = transform.position - neighbor.transform.position;
            var distance = Vector3.Magnitude(direction);
            if (distance < radius) separation += direction / (distance * distance);
        }

        var force = GetSteeringForce(separation);
        return force;
    }

    private Vector3 Alignment(GameObject[] neighbors)
    {
        var velocities = Vector3.zero;
        foreach (var neighbor in neighbors) velocities += neighbor.GetComponent<AIAgent>().movement.Velocity;
        var averageVelocity = velocities / neighbors.Length;
        var force = GetSteeringForce(averageVelocity);
        return force;
    }
}