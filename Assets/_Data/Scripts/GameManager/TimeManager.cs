using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float timeMatch = 90f; // thời gian 90s; 
    [SerializeField] bool matchInProgress = false;
    [SerializeField] MatchManager matchManager ;
    [SerializeField] TextMeshProUGUI textTimeMatch;
    private Coroutine coutdownCoroutine;
    private void Start()
    {
        StartTimeMatch();
    }
    private void StartTimeMatch()
    {
        matchInProgress = true;
        coutdownCoroutine = StartCoroutine(TrackMatchTime());
    }

    // Đếm thời gian trận đấu
    IEnumerator TrackMatchTime()
    {
        // Lấy thời gian trận đấu
        float time = 0;

        // Tăng dần thời gian
        while (time < timeMatch)
        {
            yield return null;
            
            if (matchInProgress)
            {
                time += Time.deltaTime * Time.timeScale;
                textTimeMatch.text = ConvertSecondToMinutes(time);
                //Debug.Log("Time: " + Mathf.CeilToInt(time));
            }
            else
            {
                //Debug.Log("Time: đã dừng");
            }
            // Hết 45p dừng hiệp 1
            if (Mathf.CeilToInt(time) == 45 && Mathf.RoundToInt(time - Mathf.FloorToInt(time)) == 1 && matchInProgress)
            {
                time = 45;
                textTimeMatch.text = ConvertSecondToMinutes(time);
                matchManager.EndedRound1();
                Debug.Log("Hết hiệp 1!!!!!!!!!!!!!!!!!!");
            }
        }
        // Khi hết thời gian trận đấu
        matchInProgress = false;

        Debug.Log("Match ended!");
    }

    public void SetMatchInProgress(bool matchInProgress)
    {
        this.matchInProgress = matchInProgress;
    }

    // Coroutine để tạm dừng trận đấu trong số giây nhất định
    public void PauseTimerMatch()
    {
        Debug.Log("Match resumed.");
        // Bắt đầu lại coroutine đếm thời gian nếu trận đấu vẫn chưa kết thúc
        if (!matchInProgress)
        {
            coutdownCoroutine = StartCoroutine(TrackMatchTime());
        }
    }
    private string ConvertSecondToMinutes(double time)
    {
        int minutes = (int)time;

        double fractPart = time - minutes;
        int seconds = (int)((fractPart * 100) * 60 / 100);
        //Debug.Log("Time: " + seconds);
        return $"{minutes}:{seconds:D2}";
    }
}
