using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
  public FlockController flock;
  Transform leaderTransform;

  // Update is called once per frame
  void Update()
  {
    if (flock)
    {
      if (!leaderTransform)
      {
        this.leaderTransform = flock.transform.Find("Leader Agent(Clone)");
      }

      transform.position = new Vector3(
            leaderTransform.position.x,
            transform.position.y,
            leaderTransform.position.z
        );
    }
  }
}
