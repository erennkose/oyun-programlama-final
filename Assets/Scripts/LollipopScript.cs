using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LollipopScript : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;

    UIManager uiManager;

    void Start()
    {
        uiManager = canvas.GetComponent<UIManager>();
    }

    // Lollipop'a dokunulunca oyunun bitiren fonksiyon
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            PlayerScript ps = collider.GetComponent<PlayerScript>();

            if(ps != null)
            {
                if(SceneManager.GetActiveScene().buildIndex == 1)
                {
                    SceneLoader.LoadScene(2); // Yeni sahneye geçerken asenkron yükleme
                }
                else
                {
                    uiManager.EnableWinScreen();
                    Time.timeScale = 0;
                }
            }
        }
    }
}
