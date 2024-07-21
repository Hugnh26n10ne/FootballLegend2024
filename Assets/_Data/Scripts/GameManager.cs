using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameController gameController;
    [SerializeField] TextMeshProUGUI countdownText;
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        ballController.enabled = false;
        playerController.enabled = false;
        gameController.enabled = false;
        
        CountDown(3f);

        ballController.enabled = true;
        playerController.enabled = true;
        gameController.enabled = true;
    }
    IEnumerator CountDown(float timeDelay)
    {
        countdownText.gameObject.SetActive(true);
        float countDown = timeDelay;
        while (countDown > 0)
        {
            this.countdownText.text = countDown.ToString("F0");
            yield return new WaitForSeconds(1f);
            countDown--;
        }
        this.countdownText.text = "START";
        yield return new WaitForSeconds(1f);
        countdownText.text = ""; // Xóa text sau 1 giây
        countdownText.gameObject.SetActive(false);
    }

}
