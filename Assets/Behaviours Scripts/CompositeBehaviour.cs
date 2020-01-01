using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behaviour is responsible for combining diferent behaviours
// Each behaviour is given a weight to calculate its effect on the final movement

[CreateAssetMenu(menuName = "Boid/Behaviour/Composite")]
public class CompositeBehaviour : BoidBehaviour
{
    public BoidBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, Boid boid)
    {
        // Handle data mismatch
        if(weights.Length != behaviours.Length)
            throw new UnityException("Length mismatch in behaviour array");

        // Set up move
        Vector2 move = Vector2.zero;

        // Iterate through behaviours
        for(int i = 0; i < behaviours.Length; ++i)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, boid) * weights[i];

            if(partialMove != Vector2.zero)
            {
                // Clamp the movement to the weight of the behaviour
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }
        return move;
    }
}
