using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float duration = 30f;
    public TMP_Text timerText;

    private float startTime;
    private bool started = false;
    public bool gameEnded = false;

    

    public void StartTimer()
    {
        startTime = Time.time;
        started = true;
    }

    void Update()
    {
        if (!started || gameEnded) return;

        float elapsed = Time.time - startTime;
        float remaining = Mathf.Max(0, duration - elapsed);


        if (timerText != null)
        {
            timerText.text = "Tiempo: " + Mathf.Ceil(remaining);
            
        }

        if (remaining <= 0)
        {
            EndGame();
        }

        
    }

    void EndGame()
    {
        gameEnded = true;

        GameUI ui = FindObjectOfType<GameUI>();

        if (ui != null)
        {
            ui.ShowResult(); 
        }
    }
}