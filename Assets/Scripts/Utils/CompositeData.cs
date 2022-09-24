using UnityEngine;

class CompositeData {
  public Agent[] neighbours;
  public Agent leader;
  public GameObject[] walls;

  public CompositeData(Agent[] neighbours, Agent leader, GameObject[] walls) {
    this.neighbours = neighbours;
    this.leader = leader;
    this.walls = walls;
  }
}