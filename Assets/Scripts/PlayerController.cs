using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    public CharacterController controller;

    Animator _animator;

    public Transform firePoint;
    public Transform mouseTarget;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    void Awake() => _animator = GetComponent<Animator>();

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
        _animator.SetBool("isShoot", false);
        // Reading the Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        /* Convert input into vector */
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        

        // Moving
        if (movement.magnitude > 0)
        {

            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(movement * _speed * Time.deltaTime);
        }

        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            FaceTarget();
            Shoot();
        }

        // Animating
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        /* Set variables in animator to values of inputs */
        /* 3rd and 4th argument of SetFloat() allow animations to transition smoothly */
        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    void FaceTarget()
    {
        Vector3 direction = (mouseTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        _animator.SetBool("isShoot", true);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
