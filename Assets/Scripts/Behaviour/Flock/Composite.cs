using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Composite")]
public class Composite : Behaviour
{
  public Behaviour[] behaviours;
  public float[] weights;

  public override Vector3 ComputeVelocity(Agent agent, List<Transform> neighbourTransforms, FlockController flockController)
  {
    if (behaviours.Length == 0)
    {
      Debug.LogError("No behaviour provided");
      return Vector3.zero;
    }

    Vector3 resultant = Vector3.zero;

    for (int i = 0; i < behaviours.Length; i++)
    {
      resultant += behaviours[i].ComputeVelocity(agent, neighbourTransforms, flockController) * weights[i];
    }

    return resultant;
  }
}
