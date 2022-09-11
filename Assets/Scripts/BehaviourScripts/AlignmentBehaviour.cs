using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
  public override Vector3 calculateNextMove(FlockAgent agent, List<Transform> context, Flock flock)
  {
    // If no neighbours, then we return a zero vector since we don't need any adjustments
    if (context.Count == 0)
    {
      return agent.transform.forward;
    }

    // centroid computation
    Vector3 alignmentVector = Vector3.zero;
    foreach (Transform transform in context)
    {
      alignmentVector += transform.forward;
    }
    alignmentVector /= context.Count;

    return alignmentVector;
  }
}
