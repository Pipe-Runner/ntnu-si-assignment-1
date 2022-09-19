using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Alignment")]
public class Alignment : Behaviour
{
  public override Vector3 ComputeDesiredVelocity(Agent agent, List<Agent> neighbours, FlockController flockController)
  {
    // If no one in sight, continue with same velocity as before
    if (neighbours.Count == 0)
    {
      return agent.velocity;
    }

    Vector3 result = Vector3.zero;
    foreach (Agent neighbour in neighbours)
    {
      result += neighbour.velocity;
    }
    return result / neighbours.Count;
  }
}
