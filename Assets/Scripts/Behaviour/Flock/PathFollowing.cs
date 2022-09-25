using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/PathFollowing")]
public class PathFollowing : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent,
    LeaderAgent leader,
    List<Agent> neighbours,
    List<Collider> pathIntersectionPoints,
    FlockController flockController)
  {
    Vector3 sum = Vector3.zero;

    foreach (Collider pathCollider in pathIntersectionPoints)
    {
      sum += pathCollider.transform.right * 10;
      
      if ((pathCollider.ClosestPoint(agent.transform.position) - agent.transform.position).magnitude > 5)
      {
        sum += (pathCollider.ClosestPoint(agent.transform.position) - agent.transform.position) * 4;
      }
    }
    return sum;
  }
}
