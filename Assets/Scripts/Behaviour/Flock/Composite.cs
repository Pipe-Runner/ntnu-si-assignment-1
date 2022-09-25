using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Composite")]
public class Composite : Behaviour
{
  public Behaviour[] behaviours;
  public float[] weights;

  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader, 
    List<Agent> neighbours, 
    List<Collider> wallIntersectionPoints, 
    FlockController flockController)
  {
    if (behaviours.Length == 0)
    {
      Debug.LogError("No behaviour provided");
      return Vector3.zero;
    }

    Vector3 resultantDesiredChangeVelocity = Vector3.zero;

    for (int i = 0; i < behaviours.Length; i++)
    {
      Vector3 partial = behaviours[i].ComputeDesiredVelocityChange(agent, leader, neighbours, wallIntersectionPoints, flockController) * weights[i];

      if (partial.magnitude > (weights[i] * weights[i]))
      {
        partial = partial.normalized * weights[i];
      }

      resultantDesiredChangeVelocity += partial;
    }

    // not scaling it down
    return resultantDesiredChangeVelocity;
  }
}
