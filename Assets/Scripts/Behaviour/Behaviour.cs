using UnityEngine;
using System.Collections.Generic;

public abstract class Behaviour : ScriptableObject
{
  public abstract Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    LeaderAgent leader,
    List<Agent> neighbourTransforms, 
    List<Collider> wallIntersectionPoints,
    FlockController flockController
  );
}
