using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{

    public void OnStartButton()
    {
        SceneManager.LoadScene("Game"); 
    }


    public void OnShopButton()
    {
        SceneManager.LoadScene("ShopScene"); 
    }
}