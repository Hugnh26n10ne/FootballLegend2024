using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _teamPlayers;
    [SerializeField] ScoredHandler scoredHandler;
    [SerializeField] CameraHandler cameraHandler;

    private void Awake()
    {
        _teamPlayers = new List<GameObject>();
        scoredHandler = GetComponent<ScoredHandler>();
        cameraHandler = Camera.main.GetComponent<CameraHandler>();

        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag("Player"))
            {
                _teamPlayers.Add(child.gameObject);
            }
        }
    }

    private void Start()
    {
    }

    //tạo 1 phương thức thêm thành viên vô team
    public void AddPlayerToTeam(GameObject player)
    {
        _teamPlayers.Add(player);
    }

    // tạo 1 phương thức xóa thành viên khỏi team
    public void RemovePlayerFromTeam(GameObject player)
    {
        _teamPlayers.Remove(player);
    }

    public int GetTeamCount()
    {
        return _teamPlayers.Count;
    }

    public List<GameObject> GetTeamPlayers()
    {
        return _teamPlayers;
    }
    // Cập nhật điểm số của đội
    public void UpdateScore(int score)
    {
        scoredHandler.UpdateScored(score);
    }
}
