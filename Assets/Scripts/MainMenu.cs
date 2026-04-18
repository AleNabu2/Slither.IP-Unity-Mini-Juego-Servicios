using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameManager;

    private int selectedPlayerId;

    public void PlayAsBlue()
    {
        StartGame(0);
    }

    public void PlayAsRed()
    {
        StartGame(1);
    }

    void StartGame(int playerId)
    {
        selectedPlayerId = playerId;

        PlayerPrefs.SetInt("PlayerID", playerId); 
        PlayerPrefs.Save();

        menuUI.SetActive(false);

        if (!IsSceneLoaded("Secondary"))
        {
            SceneManager.LoadScene("Secondary", LoadSceneMode.Additive);
        }

        Invoke(nameof(SetupPlayers), 0.5f);
    }

    bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
                return true;
        }
        return false;
    }

    void SetupPlayers()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        Debug.Log("Players encontrados: " + players.Length);

        foreach (var p in players)
        {
            p.SetupPlayer(selectedPlayerId);
        }

        gameManager.SetActive(true);
    }
}