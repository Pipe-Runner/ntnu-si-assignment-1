using UnityEngine;

// Required components adds the components to the game object if they are not already there when the script is added
[RequireComponent(typeof(SphereCollider))]
public class FlockAgent : MonoBehaviour
{
  SphereCollider agentCollider;

  // getter for agentCollider by doing x.AgentCollider
  public SphereCollider AgentCollider { get { return this.agentCollider; } }

  // Start is called before the first frame update
  void Start()
  {
    this.agentCollider = GetComponent<SphereCollider>();
  }

  public void Move(Vector3 velocity){
    this.transform.up = velocity;
    this.transform.position += velocity * Time.deltaTime;
  }

  // Update is called once per frame
  void Update()
  {

  }
}