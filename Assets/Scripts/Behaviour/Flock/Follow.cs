using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Follow")]
public class Follow : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Vector3> wallIntersectionPoints, 
    FlockController flockController)
  {
    Vector3 diff = (leader.transform.position - agent.transform.position);
    if(diff.magnitude < flockController.agentVisibilityRadius){
      agent.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
      return diff;
    }
    
    agent.transform.localScale = new Vector3(1f, 1f, 1f);
    return Vector3.zero;
  }
}
