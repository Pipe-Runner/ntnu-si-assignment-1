using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
  public override Vector3 calculateNextMove(FlockAgent agent, List<Transform> neighbourTransforms, Flock flock)
  {
    // If no neighbours, then we return a zero vector since we don't need any adjustments
    if (neighbourTransforms.Count == 0)
    {
      return Vector3.zero;
    }

    // centroid computation
    Vector3 avoidanceVector = Vector3.zero;
    int numAvoid = 0;
    foreach (Transform transform in neighbourTransforms)
    {
      if (Vector3.SqrMagnitude(transform.position - agent.transform.position) < flock.squareAvoidanceRadius)
      {
        numAvoid++;
        avoidanceVector += -(transform.position - agent.transform.position);
      }
    }
    if(numAvoid > 0){
      avoidanceVector /= neighbourTransforms.Count;
    }

    return avoidanceVector;
  }
}
