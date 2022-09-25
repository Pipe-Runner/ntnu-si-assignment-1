using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/LeaderFollowing")]
public class LeaderFollowing : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Collider> wallIntersectionPoints, 
    FlockController flockController)
  {
    Vector3 diff = (leader.transform.position - agent.transform.position);
    return diff;
  }
}
