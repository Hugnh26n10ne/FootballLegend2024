using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Slider slider;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        ChangeSlider();
    }

    private void ChangeSlider()
    {
        float buttonHoldTime = playerController.playerKick.GetButtonHoldTime();
        if (buttonHoldTime <= 0f)
        {
            slider.transform.gameObject.SetActive(false);
        }
        else
        {
            slider.transform.gameObject.SetActive(true);
        }
        slider.value = buttonHoldTime;
    }

}
