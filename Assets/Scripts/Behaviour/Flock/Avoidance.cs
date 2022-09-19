using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/Avoidance")]
public class Avoidance : Behaviour
{
  public override Vector3 ComputeVelocity(Agent agent, List<Transform> neighbourTransforms, FlockController flockController)
  {
    if (neighbourTransforms.Count == 0)
    {
      return Vector3.zero;
    }

    int count = 0;
    Vector3 result = Vector3.zero;
    foreach (Transform transform in neighbourTransforms)
    {
      Vector3 diff = agent.transform.position - transform.position;
      if (diff.magnitude < flockController.avoidanceRadius)
      {
        result += diff;
        count++;
      }
    }

    if (count == 0)
    {
      return Vector3.zero;
    }

    return result / count;
  }
}
