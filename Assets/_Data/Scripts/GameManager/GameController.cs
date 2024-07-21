using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public CameraHandler cameraHandler;
    public MatchManager matchManager;
    public TimeManager timeManager;

    private void Awake()
    {
        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        matchManager = GetComponent<MatchManager>();
        timeManager = GetComponent<TimeManager>();
    }
}
