using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject endPanel;
    public TMP_Text resultText;

    public PlayerScore player1;
    public PlayerScore player2;

    public void ShowResult()
    {
        endPanel.SetActive(true);

        int score1 = player1.GetScore();
        int score2 = player2.GetScore();

        resultText.text =
            "Azul: " + score1 + "\n" +
            "Rojo: " + score2 + "\n\n" +
            GetWinnerText(score1, score2);
    }

    public void ShowNetworkResult(int winner)
    {
        endPanel.SetActive(true);

        int score1 = player1.GetScore();
        int score2 = player2.GetScore();

        resultText.text =
            "Azul: " + score1 + "\n" +
            "Rojo: " + score2 + "\n\n" +
            GetWinnerText(score1, score2);
    }

    string GetWinnerText(int score1, int score2)
    {
        if (score1 > score2)
        {
            return "Ganó Azul";
        }
        else if (score2 > score1)
        {
            return "Ganó Rojo";
        }
        else
        {
            return "Empate";
        }
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}