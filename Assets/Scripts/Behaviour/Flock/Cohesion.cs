using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Cohesion")]
public class Cohesion : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Collider> wallIntersectionPoints, 
    FlockController flockController)
  {
    // If no one around, don't change velocity
    if (neighbours.Count == 0)
    {
      return Vector3.zero;
    }

    Vector3 centroid = Vector3.zero;
    foreach (Agent neighbour in neighbours)
    {
      centroid += neighbour.transform.position;
    }
    centroid /= neighbours.Count;
    return (centroid - agent.transform.position);
  }
}
