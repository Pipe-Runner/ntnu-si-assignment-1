using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Agent : MonoBehaviour
{
  SphereCollider sphereCollider;
  [HideInInspector]
  public Vector3 velocity = Vector3.zero;

  public SphereCollider GetCollider { get { return this.sphereCollider; } }

  public void ApplyForce(Vector3 force)
  {
    Vector3 acceleration = force; // considering mass to be 1
    velocity += (acceleration * Time.deltaTime * FlockController.simulationSpeed); // not following newtonian formula for computational simplification

    Move(velocity);
  }

  public void Move(Vector3 velocity)
  {
    if(velocity.magnitude != 0){
      transform.forward = velocity;
    }
    transform.position += velocity * Time.deltaTime * FlockController.simulationSpeed;
  }

  public void MoveBy(Vector3 deltaPos)
  {
    transform.forward = deltaPos;
    transform.position += deltaPos;
  }

  public void MoveTo(Vector3 position)
  {
    Vector3 facing = position - transform.position;
    transform.position = position;

    if(facing.magnitude != 0){
      transform.forward = facing;
    }
  }
}
