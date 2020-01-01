using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behaviour is responsible for the avoidance of neighbor boids
// Each boid should not collide with nearby neighbors
// This produces a movement in the oposite size of a neighbor

[CreateAssetMenu(menuName = "Boid/Behaviour/Avoidance")]
public class Avoidance : BoidBehaviour
{
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        // If there are no neighbors, do not calculate the move
        if(context.Count == 0)
            return Vector2.zero;

        // Add all points and average
        Vector3 avoidanceMove = Vector3.zero;
        int boidsToAvoid = 0;

        foreach(Transform neighbor in context)
        {
            if(Vector2.SqrMagnitude(neighbor.position - agent.transform.position) < boid.SqrAvoidanceRadius)
            {
                boidsToAvoid++;
                avoidanceMove += agent.transform.position - neighbor.position;
            }
        }

        if(boidsToAvoid > 0)
            avoidanceMove /= boidsToAvoid;

        return avoidanceMove;
    }
}
