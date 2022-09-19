using UnityEngine;
using System.Collections.Generic;

public abstract class Behaviour : ScriptableObject
{
  public abstract Vector3 ComputeDesiredVelocity(Agent agent, List<Agent> neighbourTransforms, FlockController flockController);
}
