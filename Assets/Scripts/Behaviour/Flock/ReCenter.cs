using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/ReCenter")]
public class ReCenter : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Vector3> wallIntersectionPoints, 
    FlockController flockController)
  {
    Vector3 centerPos = flockController.CenterMarker.transform.position;
    centerPos.y = flockController.height;
    Vector3 diff = (centerPos - agent.transform.position);
    if(diff.magnitude > flockController.agentVisibilityRadius){
      return diff;
    }
    
    return Vector3.zero;
  }
}
