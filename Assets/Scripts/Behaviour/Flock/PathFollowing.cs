using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Flock/PathFollowing")]
public class PathFollowing : Behaviour
{
  private Dictionary<String, bool> pathTraced = new Dictionary<String, bool>();
  private Collider activePath;
  private int oldCount = 0;

  public override Vector3 ComputeDesiredVelocityChange(
    Agent agent,
    LeaderAgent leader,
    List<Agent> neighbours,
    List<Collider> pathIntersectionPoints,
    FlockController flockController)
  {
    Vector3 sum = Vector3.zero;

    if (pathIntersectionPoints.Count != oldCount)
    {
      oldCount = pathIntersectionPoints.Count;
      foreach (Collider pathCollider in pathIntersectionPoints)
      {
        if (!pathTraced.ContainsKey(pathCollider.name))
        {
          pathTraced.Add(pathCollider.name, true);
          activePath = pathCollider;
        }
      }
    }

    if (pathIntersectionPoints.Count > 0)
    {
      Collider pathCollider = activePath;
      sum += pathCollider.transform.right * 15;

      if ((pathCollider.ClosestPoint(agent.transform.position) - agent.transform.position).magnitude > 2)
      {
        sum += (
          pathCollider.ClosestPoint(agent.transform.position) +
          (4 * pathCollider.transform.right) -
          agent.transform.position)
          * 5;
      }
    }

    return sum;
  }
}
