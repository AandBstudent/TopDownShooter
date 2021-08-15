using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hiteffectWall;
    public GameObject hiteffectEnemy; 
    float damage = 25f;
    

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        EnemyController enemy = collision.collider.GetComponent<EnemyController>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            GameObject effect = Instantiate(hiteffectEnemy, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
        else
        {
            GameObject effect = Instantiate(hiteffectWall, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
        
    }

}
