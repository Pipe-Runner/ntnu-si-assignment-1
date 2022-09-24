using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Arrival")]
public class Arrival : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Vector3> wallIntersectionPoints, 
    FlockController flockController)
  {
    return (leader.transform.position - agent.transform.position);
  }
}
