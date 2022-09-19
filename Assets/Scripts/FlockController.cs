using UnityEngine;
using System.Collections.Generic;

public class FlockController : MonoBehaviour
{
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
  public static int simulationSpeed = 8;

  [RangeAttribute(1, 10)]
  public int forceMultiplier = 5;

  [RangeAttribute(10, 50)]
  public int leaderPathRadius = 24;

  [RangeAttribute(0, 1f)]
  public float leaderSpeedMultiplier = 0.2f;

  [RangeAttribute(0, 50)]
  public int maxDesiredSpeed = 20;

  [RangeAttribute(0, 50)]
  public int agentVisibilityRadius = 15;

  // A faction of visible radius 
  [RangeAttribute(0, 1f)]
  public float avoidanceRadiusFraction = 0.3f;

  [RangeAttribute(2, 250)]
  public int agentCount = 150;

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

    // Set starting point for leader
    Vector3 leaderInitPosition = CenterMarker.transform.position + new Vector3(0, 0, leaderPathRadius);
    leaderInitPosition.y = height;

    leaderAgent = Instantiate(
         LeaderAgentPrefab,
         leaderInitPosition,
         new Quaternion(),
         this.transform
     );

    // Instantiate two sets of agents (one close to leader, one far)
    for (int i = 0; i < (agentCount / 2); i++)
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
      Vector3 oppPosLeader = CenterMarker.transform.position + new Vector3(0, 0, -leaderPathRadius) + randPoint3D;
      oppPosLeader.y = height;
      agents.Add(Instantiate(
        AgentPrefab,
        oppPosLeader,
        Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
        this.transform
      ));
    }
  }

  void Update()
  {
    // Moving leader in circle around central marker
    Vector3 newLeaderPosition = CenterMarker.transform.position + new Vector3(
      leaderPathRadius * Mathf.Sin(Time.time * leaderSpeedMultiplier * simulationSpeed),
      0
      ,
      leaderPathRadius * Mathf.Cos(Time.time * leaderSpeedMultiplier * simulationSpeed)
    );
    newLeaderPosition.y = height;
    leaderAgent.MoveTo(newLeaderPosition);

    // Move flock agents based on behaviour
    foreach (Agent agent in agents)
    {
      Vector3 desiredVelocity = behaviour.ComputeDesiredVelocity(
        agent,
        FindCollidingAgents(agent),
        this
      );

      if(desiredVelocity.magnitude > maxDesiredSpeed){
        desiredVelocity = desiredVelocity.normalized * maxDesiredSpeed;
      }

      Vector3 requiredForce = (desiredVelocity - agent.velocity);
      agent.ApplyForce(requiredForce * forceMultiplier);
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
}
