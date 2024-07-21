using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 move;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private bool isGrounded;
    private bool isFacingRight = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody>();
        moveSpeed = 8f;
        jumpForce = 30f;
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        if (!rb.isKinematic)
        {
            Move();

            Jump();

        }
        Flip();


    }

    private void FixedUpdate()
    {

        rb.AddForce(new Vector3(0, -10f, 0), ForceMode.Acceleration);
    }

    private void Move()
    {
        rb.velocity = new Vector2(move.x * moveSpeed, rb.velocity.y);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //rb.AddForce(new Vector2(rb.velocity.x,  jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f && isGrounded)
        {
            //rb.AddForce(new Vector2(rb.velocity.x,  jumpForce/2 * move.y), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.5f);

        }
    }

    private void Flip()
    {
        if (isFacingRight && move.x < 0f || !isFacingRight && move.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Transform sprite = transform.Find("Charactor").GetComponent<Transform>();
            Vector3 localScale = sprite.localScale;
            localScale.x *= -1f;
            sprite.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        float maxDistance = 1.1f;
        if (Physics.Raycast(castPos, Vector3.down, out hit, maxDistance, groundLayer))
        {
            if (hit.collider != null)
            {
                return true;
            }

        }
        //Debug.DrawRay(castPos, Vector3.down * maxDistance, Color.red);
        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        move.x = inputVector.x;
        move.y = inputVector.y;
    }
}
