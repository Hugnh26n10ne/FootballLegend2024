using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] Camera mainCamera; // Camera của người chơi

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SwitchedCamera()
    {
        
        Vector3 camPos = Camera.main.transform.position;
        Quaternion camRo = Camera.main.transform.rotation;

        camPos.z = -camPos.z;
        camRo = Quaternion.Euler(camRo.eulerAngles.x, camRo.eulerAngles.y + 180, camRo.eulerAngles.z);

        Camera.main.transform.position = camPos;
        Camera.main.transform.rotation = camRo;
    }
}
