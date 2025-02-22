using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public MovementData data;
    public virtual Vector3 Destination { get; set; }

    public virtual Vector3 Velocity { get; set; }
    public virtual Vector3 Acceleration { get; set; }
    public virtual Vector3 Direction => Velocity.normalized;
    
    public float MaxSpeed {get; set;}

    public abstract void ApplyForce(Vector3 force);

    public abstract void MoveTowards(Vector3 position);

    public virtual void Start()
    {
        MaxSpeed = data.maxSpeed;
    }
    
    public virtual void Stop()
    {
        
    }
    public virtual void Resume()
    {
        
    }
    
}