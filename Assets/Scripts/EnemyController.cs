using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius;

    public float health = 100f;

    public Transform target;
    NavMeshAgent agent;
    public Animator _animator;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 destination = agent.destination;

        // Check for player
        if (distance <= lookRadius)
        {
            Debug.Log("Player Detected");
            // Walking animation
            _animator.SetBool("isLook", true);
            // Move towards where player was detected
            agent.SetDestination(target.position);
            //Debug.Log(target.position);
            // Get and hold the destination for AI 
        }
        //else agent.SetDestination(agent.nextPosition);
        
        // Go back to being idle
        if(Vector3.Distance(destination, transform.position) <= 3)
        {
            //Debug.Log("transform position: " + transform.position);
            _animator.SetBool("isLook", false);
            //agent.SetDestination(destination);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
