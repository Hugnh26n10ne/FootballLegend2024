using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BallBounceHandler ballBounceHandler;
    public BallReceiveKick ballReceiveKick;

    private void Awake()
    {
        ballReceiveKick = GetComponent<BallReceiveKick>();
        ballBounceHandler = GetComponent<BallBounceHandler>();
    }
}
