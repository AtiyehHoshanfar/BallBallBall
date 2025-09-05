using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // برای TMP_Text

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text dragCountText;  // جایی که مقدار درگ‌ها نمایش داده میشه
    private bool isPanelActive = false;

    private int dragCount = 0; // شمارش درگ‌ها

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

            // وقتی پنل فعال میشه مقدار درگ‌ها رو نشون بده
            if (isPanelActive && dragCountText != null)
            {
                dragCountText.text = "Total Drags: " + dragCount.ToString();
            }
        }
    }

    // این تابع رو هرجا درگ اتفاق میفته صدا بزن
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
