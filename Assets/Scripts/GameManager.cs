using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playerPositionText;
    [SerializeField] private TextMeshProUGUI finishplayerPositionValue;
    [SerializeField] private TextMeshProUGUI finishplayerTimerValue;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField]private List<Transform> _checkpoints;
    
    public bool ISGameOver { private get;set; }
    

    private List<PlayerData> _players = new List<PlayerData>();
    private List<PlayerData> _sortedList;

    private int index;
    private int checkPointProximity = 20;

    public void RegisterPlayer(PlayerData player)
    {
        _players.Add(player);
    }
    
    public void ShowGameOverScreen()
    {
        foreach (var player in _players)
        {
            if (player.HasStateAuthority)
            {
                GameOverScreen.SetActive(true);
                var playerPosition = CalculatePlayerPosition(player);
                finishplayerPositionValue.text = playerPosition.ToString();

                finishplayerTimerValue.text = $"{player.RaceTime}";
                break;
            }
        }
    }

    private void Awake()
    {
        instance = this;
        GameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (ISGameOver) return;
        UpdateUI();
    }

    private int CalculatePlayerPosition(PlayerData player)
    {
        if (_players.Any(car => Vector3.Distance(car.transform.position, _checkpoints[index].position) < checkPointProximity))
        {
            _sortedList = _players.OrderBy(car => Vector3.Distance(car.transform.position, _checkpoints[index].position)).ToList();

            if (index != _checkpoints.Count - 1) index++;
        }
        else
        {
            _sortedList = _players.OrderBy(car => Vector3.Distance(car.transform.position, _checkpoints[index].position)).ToList();
        }

        // current player position
        return _sortedList.IndexOf(player) + 1;
    }

    private void UpdateUI()
    {
        foreach (var player in _players)
        {
            if (player.HasStateAuthority)
            {
                timerText.text = $"{player.RaceTime:F2}";
                
                var currentPlayerPosition = CalculatePlayerPosition(player);
                playerPositionText.text = currentPlayerPosition.ToString();
            }
        }
    }

}