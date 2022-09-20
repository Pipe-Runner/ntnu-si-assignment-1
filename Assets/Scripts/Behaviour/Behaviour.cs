using UnityEngine;
using System.Collections.Generic;

public abstract class Behaviour : ScriptableObject
{
  public abstract Vector3 ComputeDesiredVelocityChange(
    Agent agent, 
    List<Agent> neighbourTransforms, 
    List<Vector3> wallIntersectionPoints,
    FlockController flockController
  );
}
