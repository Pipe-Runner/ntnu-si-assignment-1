using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Composite")]
public class Composite : Behaviour
{
  public Behaviour[] behaviours;
  public float[] weights;

  public override Vector3 ComputeDesiredVelocity(Agent agent, List<Agent> neighbours, FlockController flockController)
  {
    if (behaviours.Length == 0)
    {
      Debug.LogError("No behaviour provided");
      return Vector3.zero;
    }

    Vector3 resultantDesiredVelocity = Vector3.zero;

    float weightSum = 0;
    for (int i = 0; i < behaviours.Length; i++)
    {
      resultantDesiredVelocity += behaviours[i].ComputeDesiredVelocity(agent, neighbours, flockController) * weights[i];
      weightSum += weights[i];
    }

    return resultantDesiredVelocity / weightSum;
  }
}
