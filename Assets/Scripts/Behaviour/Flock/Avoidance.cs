using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Avoidance")]
public class Avoidance : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Vector3> wallIntersectionPoints, 
    FlockController flockController)
  {
    if (neighbours.Count == 0)
    {
      return Vector3.zero;
    }

    int count = 0;
    Vector3 result = Vector3.zero;
    foreach (Agent neighbour in neighbours)
    {
      Vector3 diff = agent.transform.position - neighbour.transform.position;
      if (diff.magnitude < flockController.avoidanceRadius)
      {
        result += (diff) / (diff.magnitude + 0.1f );
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
