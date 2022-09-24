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
    velocity += (acceleration * Time.deltaTime); // not following newtonian formula for computational simplification

    Move(velocity);
  }

  public void Move(Vector3 velocity)
  {
    if (velocity.magnitude != 0)
    {
      transform.forward = velocity;
    }
    transform.position += velocity * Time.deltaTime;
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

    if (facing.magnitude != 0)
    {
      transform.forward = facing;
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    ContactPoint contact = collision.contacts[0];
    // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
    // Vector3 position = contact.point;
    this.velocity = this.velocity - 2 * (Vector3.Dot(this.velocity, contact.normal) * contact.normal);
  }
}
