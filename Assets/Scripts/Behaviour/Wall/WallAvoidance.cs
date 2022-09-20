using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Obstacle/Wall")]
public class WallAvoidance : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(Agent agent, List<Agent> neighbours, List<Vector3> wallIntersectionPoints, FlockController flockController)
  {
    if (wallIntersectionPoints.Count == 0)
    {
      return Vector3.zero;
    }

    int count = 0;
    Vector3 result = Vector3.zero;
    foreach (Vector3 points in wallIntersectionPoints)
    {
      Vector3 diff = agent.transform.position - points;
      if (diff.magnitude < flockController.avoidanceRadius)
      {
        result += (diff) / (diff.magnitude + 0.1f);
        count++;
      }
    }

    // if no one in avoidance radius, maintain speed
    if (count == 0)
    {
      return agent.velocity;
    }

    return (result / count);
  }
}
