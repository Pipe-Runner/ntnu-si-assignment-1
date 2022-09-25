using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/WallFollowing")]
public class WallFollowing : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent,
    LeaderAgent leader,
    List<Agent> neighbours,
    List<Collider> wallIntersectionPoints,
    FlockController flockController)
  {
    Vector3 sum = Vector3.zero;

    foreach (Collider wallCollider in wallIntersectionPoints)
    {
      sum += wallCollider.transform.right * 10;
      if ((wallCollider.ClosestPoint(agent.transform.position) - agent.transform.position).magnitude < 5)
      {
        sum += wallCollider.transform.forward * -15;
      }
      else
      {
        sum += wallCollider.transform.forward * 10;
      }
    }
    return sum;
  }
}
