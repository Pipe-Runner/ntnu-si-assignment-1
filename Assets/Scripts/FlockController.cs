using UnityEngine;

public class FlockController : MonoBehaviour
{
  public LeaderAgent LeaderAgentPrefab;
  public Agent AgentPrefab;
  public GameObject CenterMarker;

  LeaderAgent leaderAgent;


  // Start is called before the first frame update
  void Start()
  {
    leaderAgent = Instantiate(
         LeaderAgentPrefab,
         CenterMarker.transform.position + new Vector3(5, 12, 5),
         new Quaternion(),
         this.transform
     );
  }

  // Update is called once per frame
  void Update()
  {
    // Moving leader in circle
    leaderAgent.Move(
        new Vector3(
            5 * Mathf.Sin(Time.time) + CenterMarker.transform.position.x,
            12,
            5 * Mathf.Cos(Time.time) + CenterMarker.transform.position.z
        )
    );
  }
}
