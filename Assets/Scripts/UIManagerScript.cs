using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText, coinText, deathText, winText;

    [SerializeField]
    GameObject escMenuPanel, finalText;

    bool isAxisInUse = false;

    void Update()
    {
        if (Input.GetAxis("Escape") > 0.1f) // Eşik değeri
        {
            if (!isAxisInUse) // Tuşa basıldığında bir kez işlem yapılması
            {
                escMenuPanel.SetActive(!escMenuPanel.activeSelf);
                Time.timeScale = escMenuPanel.activeSelf ? 0.01f : 1f;
                isAxisInUse = true; // Tuş işleme alındı
            }
        }
        else
        {
            isAxisInUse = false; // Tuş bırakıldığında işlem tekrar mümkün hale gelir
        }
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "HP: " + health;
    }

    public void UpdateCoin(int coin, bool doubleJumpBonus)
    {
        coinText.text = "Coin: " + coin + "/128 \n" + (doubleJumpBonus == false ? "Tavsan: " + Mathf.Clamp(coin, 0, 50) + "/50" : "Çift Zıplama Aktif");
    }

    public void EnableDeathScreen()
    {
        escMenuPanel.SetActive(true);
        finalText.SetActive(true);
        deathText.gameObject.SetActive(true);
    }

    public void EnableWinScreen()
    {
        escMenuPanel.SetActive(true);
        finalText.SetActive(true);
        winText.gameObject.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneLoader.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SkipTutorial()
    {
        SceneLoader.LoadScene(2);
    }
}
