using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Alignment behaviour is responsible for aligning boids
// This means that each boid in the flock goes in the average direction

[CreateAssetMenu(menuName = "Boid/Behaviour/Alignment")]
public class Alignment : BoidBehaviour
{
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        // If there are no neighbors, maintain current heading
        if(context.Count == 0)
            return agent.transform.up;

        // Add all points and average
        Vector3 alignmentMove = Vector3.zero;

        foreach(Transform neighbor in context)
        {
            alignmentMove += neighbor.transform.up;
        }

        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
