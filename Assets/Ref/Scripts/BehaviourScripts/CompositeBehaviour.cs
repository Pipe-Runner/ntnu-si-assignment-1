using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
  public FlockBehaviour[] behaviours;
  public float[] behaviourWeights;

  public override Vector3 calculateNextMove(FlockAgent agent, List<Transform> neighbourTransforms, Flock flock)
  {
    if (behaviours.Length != behaviourWeights.Length)
    {
      Debug.LogError("The dimensions for weights and behaviours don't match: " + this.name, this);
      return Vector3.zero;
    }

    Vector3 move = Vector3.zero;
    for (int i = 0; i < behaviours.Length; i++)
    {
      Vector3 partialMove = this.behaviours[i].calculateNextMove(agent, neighbourTransforms, flock) * this.behaviourWeights[i];

      if(partialMove.magnitude > Mathf.Pow(this.behaviourWeights[i], 2)){
        partialMove = partialMove.normalized * this.behaviourWeights[i];
      }

      move += partialMove;
    }

    return move;
  }
}
