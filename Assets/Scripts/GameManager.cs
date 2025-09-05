using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text dragCountText;  
    private bool isPanelActive = false;

    private int dragCount = 0;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameOverPanel();
        }
    }

    void ToggleGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            isPanelActive = !isPanelActive;
            gameOverPanel.SetActive(isPanelActive);

            Time.timeScale = isPanelActive ? 0f : 1f;


            if (isPanelActive && dragCountText != null)
            {
                dragCountText.text = "Total Drags: " + dragCount.ToString();
            }
        }
    }


    public void RegisterDrag()
    {
        dragCount++;
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
