using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidBehaviour : ScriptableObject
{
    // The context represents the neighbours for the given boid
    public abstract Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid);
}
