using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Behaviour improves the normal Cohesion behaviour
// The normal behavioul produces a lot of jitter
// We damp that effect using SmoothDamp to smooth the direction over a given time

[CreateAssetMenu(menuName = "Boid/Behaviour/Steered")]
public class SteeredCohesion : BoidBehaviour
{

    Vector2 velocity;
    public float agentSmoothTime = .5f;

     public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        // If there are no neighbors, do not calculate the move
        if(context.Count == 0)
            return Vector2.zero;

        // Add all points and average
        Vector3 cohesionMove = Vector3.zero;

        foreach(Transform neighbor in context)
        {
            cohesionMove += neighbor.position;
        }

        cohesionMove /= context.Count;

        // Create offset from agent position
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref velocity, agentSmoothTime);
        return cohesionMove;
    }
}
