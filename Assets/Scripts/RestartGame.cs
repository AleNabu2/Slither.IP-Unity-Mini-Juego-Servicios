using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject endPanel;

    public void Restart()
    {
        if (endPanel != null)
        {
            endPanel.SetActive(false); 
        }

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}