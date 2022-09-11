using UnityEngine;
using System.Collections.Generic; // needed to use List

public abstract class FlockBehaviour : ScriptableObject
{
    /**
    context is the list of transforms of all the neighbors,
    we could have passes the flock agent itself, but we want to keep it generic
    so that in future we can also pass in obstacles or other things.
    */
    public abstract Vector3 calculateNextMove(FlockAgent agent, List<Transform> context, Flock flock);
}
