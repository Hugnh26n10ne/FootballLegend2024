using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private bool isSwitchedInput = false;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if (!isSwitchedInput)
        {
            inputVector.x = Input.GetAxis("Horizontal");
        }
        else
        {
            inputVector.x = Input.GetAxis("Horizontal") * -1;
        }
        inputVector.y = Input.GetAxis("Vertical");
        playerController.playerMovement.SetInputVector(inputVector);
    }

    public void SetStatusSwitchInput(bool isSwitchedInput)
    {
        this.isSwitchedInput = isSwitchedInput;
    }
}
