using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] List<TeamManager> teamManagers;
    [SerializeField] GameObject ball;
    [SerializeField] List<GameObject> players = new List<GameObject>();
    [SerializeField] Vector3 initialBallPos;
    [SerializeField] List<Vector3> initialPlayerPos = new List<Vector3>();
    [SerializeField] OnGoalToUpdateScored onGoalToUpdateScored;

    private Rigidbody ballRigidbody;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameController gameController;

    private void Awake()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
        gameController = GetComponent<GameController>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        onGoalToUpdateScored = ball.GetComponentInChildren<OnGoalToUpdateScored>();
        ball.GetComponent<BallController>().ballBounceHandler.SetBounce(0.8f);
    }
    private void Start()
    {
        GetPlayerAndBallPos();
    }

    public void GetPlayerAndBallPos()
    {
        // Clear danh sách trước khi thêm mới
        players.Clear();
        initialPlayerPos.Clear();

        // lấy vị trí của các player
        foreach (var team in teamManagers)
        {
            var teamPlayers = team.GetTeamPlayers();
            if (teamPlayers != null)
            {
                players.AddRange(teamPlayers);
            }
        }

        try
        {
            foreach (var player in players)
            {
                if (player != null && player.gameObject != null)
                {
                    initialPlayerPos.Add(player.gameObject.transform.position);
                }
                else
                {
                    initialPlayerPos.Add(Vector3.zero);
                }
            }
        }
        catch
        {
            foreach (var player in players)
            {
                initialPlayerPos.Add(Vector3.zero);
            }
        }

        if (ball != null)
        {
            // lấy vị trí quả bóng
            initialBallPos = ball.transform.position;
        }
    }

    private void OnApplicationQuit()
    {
        // Clear danh sách khi ứng dụng thoát
        players.Clear();
        initialPlayerPos.Clear();
    }

    private void OnDisable()
    {
        // Clear danh sách khi đối tượng bị hủy
        players.Clear();
        initialPlayerPos.Clear();
    }
    public void OnGoalScored()
    {
        // Chờ 5s sau mới đặt lại trạng thái thi đấu
        StartCoroutine(WaitForDelayTimeAfterGoal(5f));
        // Có thể cần đặt lại các trạng thái khác, ví dụ như trạng thái di chuyển của các cầu thủ
        Debug.Log("Goal scored! Positions reset.");
    }

    public void EndedRound1()
    {
        StartCoroutine(WaitForDelayTimeAfterRound1(5f));
    }

    
    IEnumerator WaitForDelayTimeAfterRound1(float timeDelay)
    {
        // Dừng hoạt động trận đấu
        StopMatch();
        // Chạy hiệu ứng 5s

        // Đổi sân
        gameController.cameraHandler.SwitchedCamera();
        // Đổi hướng di chuyển
        playerController.playerInputHandler.SetStatusSwitchInput(true);

        yield return new WaitForSeconds(timeDelay);

        // Tiếp tục trận đấu
        StartMatch();


    }
    IEnumerator WaitForDelayTimeAfterGoal(float timeDelay)
    {
        // Dừng hoạt động 
        StopMatch();

        yield return new WaitForSeconds(timeDelay);
        // Tiếp tục trận đấu
        StartMatch();
    }

    public void StopMatch()
    {
        // Dừng bộ đếm thời gian
        gameController.timeManager.SetMatchInProgress(false);
        // Dừng không cho nhân vật di chuyển
        playerController.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //playerController.playerMovement.enabled = false;
        
        // Đặt lại vận tốc của bóng
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.GetComponent<BallController>().ballBounceHandler.SetBounce(0);
        }
    }
    public void StartMatch()
    {
        // Đặt lại vị trí của bóng
        if (ball != null)
        {
            ball.GetComponent<BallController>().ballBounceHandler.SetBounce(0.8f);
            ball.transform.position = initialBallPos;
        }

        // Đặt lại vị trí của các cầu thủ
        if (players != null)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].transform.position = initialPlayerPos[i];
            }
        }

        // Bắt đầu tiếp tục trận đấu
        gameController.timeManager.SetMatchInProgress(true);

        // Bắt đầu tính điểm
        onGoalToUpdateScored.SetStatusApplyGoal(true);

        // Cho nhân vật di chuyển trở lại
        playerController.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //playerController.playerMovement.enabled = true;
    }
}
