using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ApiClient api;

    private List<PlayerController> players = new List<PlayerController>();

    [SerializeField] private float sendInterval = 0.2f;
    [SerializeField] private float receiveInterval = 0.2f;

    private int localPlayerId;

    public string gameId;

    private bool timerStarted = false;
    

    void Awake()
    {
        localPlayerId = PlayerPrefs.GetInt("PlayerID", 0);
        Debug.Log("LOCAL PLAYER ID: " + localPlayerId);
    }

    void Start()
    {
        StartCoroutine(SetupGame());
    }

    IEnumerator SetupGame()
    {
        yield return new WaitForSeconds(0.5f);

        FindPlayers();

        api.OnDataReceived += OnDataReceived;

        InvokeRepeating(nameof(SendLocalPlayer), 0f, sendInterval);
        InvokeRepeating(nameof(GetRemotePlayer), 0f, receiveInterval);
    }

    void FindPlayers()
    {
        players = new List<PlayerController>();

        PlayerController[] found = FindObjectsOfType<PlayerController>();

        foreach (var p in found)
        {
            int id = p.GetPlayerId();

            while (players.Count <= id)
            {
                players.Add(null);
            }

            players[id] = p;
        }

        Debug.Log("Players encontrados: " + players.Count);
    }

    void SendLocalPlayer()
    {
        if (players.Count <= localPlayerId || players[localPlayerId] == null) return;

        Vector3 position = players[localPlayerId].GetPosition();

        ServerData data = new ServerData
        {
            posX = position.x,
            posY = position.y,
            posZ = position.z
        };

        StartCoroutine(api.PostPlayerData(gameId, localPlayerId.ToString(), data));
    }

    void GetRemotePlayer()
    {
        int remotePlayerId = (localPlayerId == 0) ? 1 : 0;

        if (players.Count <= remotePlayerId) return;

        StartCoroutine(api.GetPlayerData(gameId, remotePlayerId.ToString()));
    }

    public void OnDataReceived(int playerId, ServerData data)
    {

        
        if (!timerStarted && playerId != localPlayerId)
        {
            timerStarted = true;
            StartGameTimer();
        }

        if (playerId == localPlayerId) return;

        Vector3 position = new Vector3(data.posX, data.posY, data.posZ);

        if (players.Count > playerId && players[playerId] != null)
        {
            players[playerId].MovePlayer(position);
        }



    }

    void StartGameTimer()
    {
        GameTimer timer = FindObjectOfType<GameTimer>();

        if (timer != null)
        {
            timer.StartTimer(); // 🔥 ambos lo llaman cuando detectan al otro
        }
    }


    void ShowWinner(int winner)
    {
        GameUI ui = FindObjectOfType<GameUI>();

        if (ui != null)
        {
            ui.ShowNetworkResult(winner);
        }
    }
}