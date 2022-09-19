using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Cohesion")]
public class Cohesion : Behaviour
{
  public override Vector3 ComputeVelocity(Agent agent, List<Transform> neighbourTransforms, FlockController flockController)
  {
    if (neighbourTransforms.Count == 0)
    {
      return Vector3.zero;
    }

    Vector3 centroid = Vector3.zero;
    foreach (Transform transform in neighbourTransforms)
    {
      centroid += transform.position;
    }
    centroid /= neighbourTransforms.Count;
    return (centroid - agent.transform.position);
  }
}
