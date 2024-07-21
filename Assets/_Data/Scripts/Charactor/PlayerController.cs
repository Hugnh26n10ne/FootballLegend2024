using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public PlayerKick playerKick;
    public PlayerMovement playerMovement;
    public PlayerUI playerUI;

    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerKick = GetComponent<PlayerKick>();
        playerMovement = GetComponent<PlayerMovement>();
        playerUI = GetComponent<PlayerUI>();
    }
}
