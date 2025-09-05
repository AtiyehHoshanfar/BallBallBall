using UnityEngine;
using UnityEngine.SceneManagement; // برای Load کردن Scene

public class MainMenuController : MonoBehaviour
{
    // تابع برای دکمه Start
    public void OnStartButton()
    {
        SceneManager.LoadScene("Game"); // اسم Scene بازی که تا حالا کار کردیم
    }

    // تابع برای دکمه Shop
    public void OnShopButton()
    {
        SceneManager.LoadScene("ShopScene"); // بعدا وقتی Shop رو ساختی اسمش بذار
    }
}