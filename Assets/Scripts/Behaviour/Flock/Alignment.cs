using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Alignment")]
public class Alignment : Behaviour
{
  public override Vector3 ComputeVelocity(Agent agent, List<Transform> neighbourTransforms, FlockController flockController)
  {
    if (neighbourTransforms.Count == 0)
    {
      return agent.transform.forward;
    }

    Vector3 result = Vector3.zero;
    foreach (Transform transform in neighbourTransforms)
    {
      result += transform.forward;
    }
    return result / neighbourTransforms.Count;
  }
}
