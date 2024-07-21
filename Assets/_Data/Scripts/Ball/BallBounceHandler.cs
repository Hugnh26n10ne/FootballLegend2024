using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounceHandler : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    PhysicMaterial material;
    Vector3 lastVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        if (col != null)
        {
            material = col.sharedMaterial;
        }
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        speed = speed * material.bounciness;
        rb.velocity = direction * Mathf.Max(speed, 0);
    }

    public void SetBounce(float valueBounce)
    {
        material.bounciness = valueBounce;
    }
}
