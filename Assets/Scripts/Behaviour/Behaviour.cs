using UnityEngine;
using System.Collections.Generic;

public abstract class Behaviour : ScriptableObject
{
  public abstract Vector3 ComputeVelocity(Agent agent, List<Transform> neighbourTransforms, FlockController flockController);
}
