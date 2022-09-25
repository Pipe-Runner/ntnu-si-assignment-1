using UnityEngine;
using System.Collections.Generic;

public class FlockController : MonoBehaviour
{
  public enum SpecialCase { None, WallFollowing, PathFollowing };

  public enum PathType { SimpleCircle, SimpleLine };

  public enum DeltaType { ForceBased, VelocityBased };

  public PathType path = PathType.SimpleCircle;

  public DeltaType deltaType = DeltaType.ForceBased;

  public SpecialCase specialCase = SpecialCase.None;

  public LeaderAgent LeaderAgentPrefab;
  public Agent AgentPrefab;
  public GameObject CenterMarker;
  public Behaviour behaviour;

  /* -------------------------------------------------------------------------- */
  /*                               TUNABLE PARAMS                               */
  /* -------------------------------------------------------------------------- */

  [RangeAttribute(0, 10)]
  public int height = 10;

  [RangeAttribute(1, 10)]
  public int forceMultiplier = 5;

  [RangeAttribute(10, 50)]
  public int leaderPathRadius = 24;

  [RangeAttribute(0, 1f)]
  public float leaderSpeedMultiplier = 0.1f;

  [RangeAttribute(0, 10f)]
  public float maxDesiredSpeed = 0.1f;

  [RangeAttribute(0, 10f)]
  public float maxForce = 0.05f;

  [RangeAttribute(0, 50)]
  public int agentVisibilityRadius = 15;

  // A faction of visible radius 
  [RangeAttribute(0, 1f)]
  public float avoidanceRadiusFraction = 0.3f;

  [RangeAttribute(1, 200)]
  public int agentCount = 200;

  public bool multiFlock = false;

  // Higher density, closer to the leader
  [RangeAttribute(1, 10)]
  public int spawnDensity = 8;

  LeaderAgent leaderAgent;
  List<Agent> agents = new List<Agent>();

  // Computing for cache
  [HideInInspector]
  public float avoidanceRadius;

  void Start()
  {
    avoidanceRadius = agentVisibilityRadius * avoidanceRadiusFraction;

    if ((specialCase == SpecialCase.WallFollowing) || (specialCase == SpecialCase.PathFollowing))
    {
      Vector3 position = new Vector3(426.6f, height, 209.2f);
      position.y = height;
      Agent agent = Instantiate(
        AgentPrefab,
        position,
        Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
        this.transform
      );
      agent.transform.forward = new Vector3(436.9f, height, 219.7f);
      agents.Add(agent);
      return;
    }

    // Set starting point for leader
    Vector3 leaderInitPosition = CenterMarker.transform.position + new Vector3(leaderPathRadius, 0, 0);
    leaderInitPosition.y = height;

    leaderAgent = Instantiate(
         LeaderAgentPrefab,
         leaderInitPosition,
         new Quaternion(),
         this.transform
     );

    // Instantiate two sets of agents (one close to leader, one far)
    for (int i = 0; i < (multiFlock ? (agentCount / 2) : agentCount); i++)
    {
      Vector2 randPointCloseToLeader = spawnDensity * Random.insideUnitCircle;
      Vector3 randPoint3D = new Vector3();
      randPoint3D.x = randPointCloseToLeader.x;
      randPoint3D.y = 0;
      randPoint3D.z = randPointCloseToLeader.y;

      // Close Agent
      agents.Add(Instantiate(
        AgentPrefab,
        leaderInitPosition + randPoint3D,
        Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
        this.transform
      ));

      // Far Agent
      if (multiFlock)
      {
        Vector3 oppPosLeader = CenterMarker.transform.position + new Vector3(leaderPathRadius, 0, 0) + randPoint3D;
        oppPosLeader.y = height;
        agents.Add(Instantiate(
          AgentPrefab,
          oppPosLeader,
          Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
          this.transform
        ));
      }
    }
  }

  void Update()
  {
    float time = Time.timeSinceLevelLoad;

    if (specialCase == SpecialCase.None)
    {
      switch (path)
      {
        case PathType.SimpleCircle:
          {
            // Moving leader in circle around central marker
            Vector3 newLeaderPosition = CenterMarker.transform.position + new Vector3(
              leaderPathRadius * Mathf.Cos(time * leaderSpeedMultiplier),
              0
              ,
              leaderPathRadius * Mathf.Sin(time * leaderSpeedMultiplier)
            );
            newLeaderPosition.y = height;
            leaderAgent.MoveTo(newLeaderPosition);
            break;
          }
        case PathType.SimpleLine:
          {
            // Moving leader in straight line
            Vector3 newLeaderPosition = CenterMarker.transform.position + new Vector3(
              leaderPathRadius * Mathf.Cos(time * leaderSpeedMultiplier),
              0
              ,
              0
            );
            newLeaderPosition.y = height;
            leaderAgent.MoveTo(newLeaderPosition);
            break;
          }
      }

      foreach (Agent agent in agents)
      {
        Vector3 desiredVelocityChange = behaviour.ComputeDesiredVelocityChange(
          agent,
          leaderAgent,
          FindCollidingAgents(agent),
          new List<Collider>(),
          this
        );

        if (desiredVelocityChange.magnitude > maxDesiredSpeed)
        {
          desiredVelocityChange = desiredVelocityChange.normalized * maxDesiredSpeed;
        }

        // Not accurate
        if (deltaType == DeltaType.ForceBased)
        {
          Vector3 requiredForce = desiredVelocityChange * forceMultiplier;

          if (requiredForce.magnitude > maxForce)
          {
            requiredForce = requiredForce.normalized * maxForce;
          }

          agent.ApplyForce(requiredForce);
        }
        else
        {
          agent.Move(agent.velocity + desiredVelocityChange);
        }
      }
    }
    else if(specialCase == SpecialCase.WallFollowing || specialCase == SpecialCase.PathFollowing){
      foreach (Agent agent in agents)
      {
        List<Collider> walls = FindCollidingWallsTransforms(agent);

        Vector3 desiredVelocityChange = behaviour.ComputeDesiredVelocityChange(
          agent,
          leaderAgent,
          FindCollidingAgents(agent),
          walls,
          this
        );

        agent.Move((agent.velocity + desiredVelocityChange) * 5);
      }
    }
  }

  List<Agent> FindCollidingAgents(Agent agent)
  {
    Collider[] colliders = Physics.OverlapSphere(agent.transform.position, agentVisibilityRadius);
    List<Agent> result = new List<Agent>();

    foreach (Collider collider in colliders)
    {
      if (collider != agent.GetCollider && collider.gameObject.tag == "Agent")
      {
        result.Add(collider.gameObject.GetComponent<Agent>());
      }
    }

    return result;
  }

  List<Collider> FindCollidingWallsTransforms(Agent agent)
  {
    Collider[] colliders = Physics.OverlapSphere(agent.transform.position, agentVisibilityRadius);
    List<Collider> result = new List<Collider>();

    foreach (Collider collider in colliders)
    {
      if (collider != agent.GetCollider && collider.gameObject.tag == "wall")
      {
        Debug.Log(collider.contactOffset);
        result.Add(collider);
      }
    }

    return result;
  }
}
