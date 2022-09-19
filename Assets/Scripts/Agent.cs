using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Agent : MonoBehaviour
{
  SphereCollider sphereCollider;

  public SphereCollider GetCollider { get { return this.sphereCollider; } }

  public void Move(Vector3 velocity)
  {
    transform.forward = velocity;
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
    transform.forward = facing;
  }
}
