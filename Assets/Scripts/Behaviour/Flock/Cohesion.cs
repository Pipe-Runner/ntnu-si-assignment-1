using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Cohesion")]
public class Cohesion : Behaviour
{
  public override Vector3 ComputeDesiredVelocity(Agent agent, List<Agent> neighbours, FlockController flockController)
  {
    // If no one around, don't change velocity
    if (neighbours.Count == 0)
    {
      return agent.velocity;
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
