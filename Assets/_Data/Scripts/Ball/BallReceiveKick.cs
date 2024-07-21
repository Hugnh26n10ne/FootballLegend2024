using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReceiveKick : MonoBehaviour
{

    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0f, -10f, 0f),ForceMode.Acceleration);
    }

    public void ApplyForceKickBall(float kickForce , Vector2 direction, float timeForce, float angle)
    {
        rb.AddForce(kickForce * direction + new Vector2(0f, angle * timeForce), ForceMode.Impulse);
    }
}
