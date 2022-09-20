using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Alignment")]
public class Alignment : Behaviour
{
  public override Vector3 ComputeDesiredVelocityChange(Agent agent, List<Agent> neighbours, List<Vector3> wallIntersectionPoints, FlockController flockController)
  {
    // If no one in sight, continue with same velocity as before
    if (neighbours.Count == 0)
    {
      return Vector3.zero;
    }

    Vector3 result = Vector3.zero;
    foreach (Agent neighbour in neighbours)
    {
      result += neighbour.transform.forward;
    }
    return (result / neighbours.Count);
  }
}
