using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
  public override Vector3 calculateNextMove(FlockAgent agent, List<Transform> neighbourTransforms, Flock flock)
  {
    // If no neighbours, then we return back original direction
    if (neighbourTransforms.Count == 0)
    {
      return agent.transform.up;
    }

    // centroid computation
    Vector3 alignmentVector = Vector3.zero;
    foreach (Transform transform in neighbourTransforms)
    {
      alignmentVector += transform.up;
    }
    alignmentVector /= neighbourTransforms.Count;

    return alignmentVector;
  }
}
