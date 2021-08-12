using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    public float health = 100f;

    Transform target;
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
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 destination = agent.destination;
 
        if(distance <= lookRadius)
        {
            Debug.Log("Player Detected");
            _animator.SetBool("isLook", true);
            agent.SetDestination(target.position);
            destination = agent.destination;
        }
        
        // Go back to being idle
        if(Vector3.Distance( destination, transform.position) <= 2)
        {
            _animator.SetBool("isLook", false);
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
