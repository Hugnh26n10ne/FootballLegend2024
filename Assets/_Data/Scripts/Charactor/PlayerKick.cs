using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Presets;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] float maxAngle = 10f;
    [SerializeField] float baseForceMultiplier = 10f;
    [SerializeField] float playerStrength = 75f;
    [SerializeField] float buttonHoldTime = 0f;
    [SerializeField] float buttonMaxHoldTime = 1f;
    [SerializeField] bool isButtonHeld = false;
    [SerializeField] bool isReset = true;
    [SerializeField] bool isKick = false;

    [SerializeField] float distance;
    [SerializeField] Transform ball;

    private void Awake()
    {
        ball = GameObject.FindWithTag("Ball").gameObject.transform;
    }

    private void Update()
    {
        distance = (transform.position - ball.position).magnitude; //  tính khoảng cách từ cầu thủ đến quả bóng
        
        ButtonHoldHandler();
    }

    private void ButtonHoldHandler()
    {
        if (Input.GetKeyDown(KeyCode.J) && isReset)
        {
            buttonHoldTime = 0f;
            isButtonHeld = true;
        }
        if (isButtonHeld)
        {
            buttonHoldTime += Time.deltaTime;
            buttonHoldTime = Mathf.Clamp(buttonHoldTime, 0f, 1f);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            isButtonHeld = false;
            isKick = true;
            StartCoroutine(WaitResetForce());
        }
    }

    IEnumerator WaitResetForce()
    {
        isReset = false; // Ngăn không cho reset lực mới
        yield return new WaitForSeconds(1.0f);
        if (!isButtonHeld) // Chỉ reset thời gian khi nút không bị giữ
        {
            buttonHoldTime = 0f; // Reset thời gian nhấn nút về 0
        }
        isReset = true; // Cho phép reset lực mới
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && distance < 4f && isKick)
        {
            collision.gameObject.GetComponent<BallReceiveKick>().ApplyForceKickBall(GetForceToBall(buttonHoldTime), GetDirection(), (float)buttonHoldTime / buttonMaxHoldTime, (float)maxAngle);
        }
    }

    private float GetForceToBall(float holdTime)
    {
        float baseForce = holdTime * baseForceMultiplier;
        float totalForce = baseForce * (1 + playerStrength / 100); // tính tổng lực dựa trên sức mạnh cầu thủ
                                                                   // Debug.Log("Lực truyền: " + totalForce);
        return totalForce;
    }

    private Vector2 GetDirection()
    {
        Vector2 launchVector = transform.Find("Charactor").GetComponent<Transform>().localScale; // Xác định hướng bắn theo hướng nhân vật
        //Debug.Log("Góc: " + launchVector);
        return launchVector;
    }

    public float GetButtonHoldTime()
    {
        return buttonHoldTime;
    }

}
