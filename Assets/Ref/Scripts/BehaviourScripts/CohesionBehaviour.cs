using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu adds this Class in the Unit Context Menu itself
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
  // the override keyword needs to be used to implement abstract classes
  public override Vector3 calculateNextMove(FlockAgent agent, List<Transform> neighbourTransforms, Flock flock)
  {
    // If no neighbours, then we return a zero vector since we don't need any adjustments
    if (neighbourTransforms.Count == 0)
    {
      return Vector3.zero;
    }

    // centroid computation
    Vector3 centroid = Vector3.zero;
    foreach (Transform transform in neighbourTransforms)
    {
      centroid += transform.position;
    }
    centroid /= neighbourTransforms.Count;

    // compute position vector delta
    return centroid - agent.transform.position;
  }
}
