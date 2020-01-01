using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public BoidAgent agentPrefab;
    List<BoidAgent> agents = new List<BoidAgent>();
    public BoidBehaviour behaviour;

    [Range(10, 500)]
    public int startingCount = 200;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    // Square roots require heavy computations, so we can use the squared magnitude instead
    float sqrMaxSpeed;
    float sqrNeighborRadius;
    float sqrAvoidanceRadius;
    public float SqrAvoidanceRadius { get { return sqrAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        sqrMaxSpeed = maxSpeed * maxSpeed;
        sqrNeighborRadius = neighborRadius * neighborRadius;
        sqrAvoidanceRadius = sqrNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; ++i)
        {
            BoidAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );

            newAgent.name = "Agent_" + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(BoidAgent agent in agents)
        {
            List<Transform> context = GetNearbyBoids(agent);

            // In order to check this functionality, we can change the color of the boid depending on the number of neighbors
            // This is really slow and should only be used for debugging purposes
            // const float numNeighbors = 6f;
            // agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / numNeighbors);

            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > sqrMaxSpeed)
                move = move.normalized * maxSpeed;

            agent.Move(move);
        }
    }

    // In order to get the neighbors boids, we can iterate through every boid in the scene
    // and check which are close to the current boid.
    // However we can use Unity's physics system to return all boids which collide with the current boid.
    List<Transform> GetNearbyBoids(BoidAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
