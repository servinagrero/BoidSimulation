using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Each boid requires to have a Collider2D attached
// The collider is used to check for neighbors
[RequireComponent(typeof(Collider2D))]
public class BoidAgent : MonoBehaviour
{
    // We cannot name the Collider2D 'collider' since the Collider2D has a deprecated attribute with the same name
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent will find the instance of the specified object attached to this instance
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector3 delta_pos)
    {
        // Point the boid into the given direction
        transform.up = delta_pos;
        transform.position += delta_pos * Time.deltaTime;
    }
}
