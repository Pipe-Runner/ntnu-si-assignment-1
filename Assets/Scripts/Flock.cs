using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
The role of Flock is to:
- Instantiate the scene with Agent prefabs
- Running the epochs and handling the behaviors of the agents
*/
public class Flock : MonoBehaviour
{
  public FlockAgent agentPrefab;
  List<FlockAgent> agents = new List<FlockAgent>();
  public FlockBehaviour behaviour;

  // There square brackets are hints for Unity UI
  [Range(0, 500)]
  public int startingCount = 250;
  const float agentDensity = 0.08f;

  /**
	This is a factor multiplied to the entire motion of agent to make them move faster.
	This is added due to the fact that in emergent behaviour, the forces that we get are usually very tiny
	so the simulation runs a lot slow
	*/
  [Range(1f, 100f)]
  public float driveFactor = 10f;

  /**
	Even though we are multiplying a the velocity of each agent with the driveFactor, we don't 
	want the speed to exceed a certain cap.
	*/
  [Range(1f, 100f)]
  public float maxSpeed = 5f;

  /**
	The box collider radius for each agent
	*/
  [Range(1f, 10f)]
  public float neighbourRadius = 5f;

  /**
	The parameter for avoidance (dead zone within which another agent should not be present)
	*/
  [Range(0f, 1f)]
  public float avoidanceRadiusMultiplier = 0.5f;

  // precomputing a few values to make sure we avoid verbosity for calculations
  float squareMaxSpeed;
  float squareNeighbourRadius;
  public float squareAvoidanceRadius;
  float SquareAvoidanceRadius { get { return this.squareAvoidanceRadius; } }

  // Start is called before the first frame update
  void Start()
  {
    this.squareMaxSpeed = this.maxSpeed * this.maxSpeed;
    this.squareNeighbourRadius = this.neighbourRadius * this.neighbourRadius;
    this.squareAvoidanceRadius = this.squareNeighbourRadius * Mathf.Pow(this.avoidanceRadiusMultiplier, 2);

    // Instantiate Objects from Prefab
    for (int i = 0; i < this.startingCount; i++)
    {
      FlockAgent agent = Instantiate(
        agentPrefab,
        (Vector3)(Random.insideUnitCircle * this.startingCount * agentDensity),
        Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
        this.transform
      );

      agent.name = "Agent " + i;
      agents.Add(agent);
    }
  }

  // Update is called once per frame
  void Update()
  {
    foreach (FlockAgent agent in agents)
    {
      List<Transform> neighbourTransforms = this.GetNearbyAgents(agent);
      Vector3 move = this.behaviour.calculateNextMove(agent, neighbourTransforms, this);
      move *= this.driveFactor;
      if (move.sqrMagnitude > this.squareMaxSpeed)
      {
        move = move.normalized * this.maxSpeed;
      }
      agent.Move(move);
    }
  }

  List<Transform> GetNearbyAgents(FlockAgent agent)
  {
    List<Transform> neighbourTransforms = new List<Transform>();
    Collider[] neighbourColliders = Physics.OverlapSphere(agent.transform.position, this.neighbourRadius);

    foreach (Collider neighbourCollider in neighbourColliders)
    {
      // we check if the collider instance is of the agent we are checking the neighbours for
      // we don't want to add that to our list since it will be the same object
      if (agent.AgentCollider != neighbourCollider)
      {
        neighbourTransforms.Add(neighbourCollider.transform);
      }
    }
    return neighbourTransforms;
  }
}
