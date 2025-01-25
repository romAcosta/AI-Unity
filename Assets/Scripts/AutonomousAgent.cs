using System;
using UnityEditor.Searcher;
using UnityEngine;
using Random = UnityEngine.Random;


public class AutonomousAgent : AIAgent
{
    public AutonomousAgentData data;

    [Header("Perception")]
    public Perception seekPerception;
    public Perception fleePerception;
    public Perception flockPerception;
    
    private float angle;
    
    public void Update()
    {
        
        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
        Flee();
        Seek();
        Flock();
        Wander();
        if (movement.Acceleration.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement.Direction, Vector3.up);
        }
    }

    
    private Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);
        return force;
    }
    private Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position ;
        Vector3 force = GetSteeringForce(direction);
        return force;
    }
    
    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.data.maxSpeed;
        Vector3 steeringForce = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steeringForce, movement.data.maxForce);
        
        return force;
    }

    void Wander()
    {
        //WANDER
        if (movement.Acceleration.sqrMagnitude == 0)
        {
            angle += Random.Range(-data.displacement, data.displacement);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 point = rotation * (Vector3.forward * data.radius);
            Vector3 forward = movement.Direction * data.distance;
            Vector3 force = GetSteeringForce(point + forward);
            movement.ApplyForce(force);
        }
    }

    void Flee()
    {
        //Flee
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
    }

    void Seek()
    {
        //SEEK
        if(seekPerception != null){
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
    }

    void Flock()
    {
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects) * data.cohesionWeight);
                movement.ApplyForce(Separation(gameObjects));
                movement.ApplyForce(Alignment(gameObjects));
                
            }
        }
    }

    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }
        
        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;
        Vector3 force = GetSteeringForce(direction);
        print(force);
        return force;
    }
    private Vector3 Separation(GameObject[] neighbors)
    {
        return Vector3.zero;
    }
    private Vector3 Alignment(GameObject[] neighbors)
    {
        return Vector3.zero;
    }
}
