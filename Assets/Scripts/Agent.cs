using UnityEngine;

public class Agent : MonoBehaviour
{
  public void Move(Vector3 position)
  {
    transform.position = position;
  }
}
