using UnityEngine;

public class KinematicMovement : Movement
{
    private void LateUpdate()
    {
        Velocity += Acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, data.maxSpeed);

        transform.position += Velocity * Time.deltaTime;
        Acceleration = Vector3.zero;
    }

    public override void ApplyForce(Vector3 force)
    {
        Acceleration += force.normalized * data.maxForce;
    }

    public override void MoveTowards(Vector3 position)
    {
        var direction = position - transform.position;
        ApplyForce(direction.normalized * data.maxForce);
    }
}