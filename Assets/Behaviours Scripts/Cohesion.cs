using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behaviour is responsible for grouping boids
// A boid should fly next te nearby boids
// This means, move the boid in the average position of the flock

[CreateAssetMenu(menuName = "Boid/Behaviour/Cohesion")]
public class Cohesion : BoidBehaviour
{
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
        return cohesionMove;
    }
}
