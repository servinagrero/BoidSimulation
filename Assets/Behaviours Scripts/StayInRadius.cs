using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behaviour is responsible for grouping the boids in the screen
// Tries to keep the boids in a circle while maintaining some natural movement

[CreateAssetMenu(menuName = "Boid/Behaviour/StayInRadius")]
public class StayInRadius : BoidBehaviour
{
    public Vector2 center = Vector2.zero;
    public float radius = 15f;

    // This threshold is how far from the circle perimeter a boid needs to be to be redirected
    // A value of 1.0f means the circumference itself
    public float radiusThreshold = 0.2f;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        // Calculate the distance from the boid to the center of the circle
        // This circle corresponds with the center of the viewpoint
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        if( t < radiusThreshold)
            return Vector2.zero;
        else
            return centerOffset * t * t;
    }

}
