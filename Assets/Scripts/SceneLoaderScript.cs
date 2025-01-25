using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Bu scripti Sigleton olarak kullanmak için static değişken atıyoruz. 
    public static SceneLoader instance;

    // Sahne yüklendiğinde kendisinden başka var mı diye kontrol edilen hiç oluşmamışsa oluşturan fonksiyon
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Değiştireceğimiz sahnenin IDsini tutacak değişken
    static private int index = -1;

    // private olan değişkeni değiştirmek için bir setter
    static public void LoadScene(int i)
    {
        index = i;
    }

    void Update()
    {
        // index değişkenimiz 0 dan büyük olursa içine girip sahneyi yüklemek için gerekli olan Coroutine yi çağırıyor
        if(index >= 0)
        {
            StartCoroutine(LoadSceneEnum(index));
            index = -1;
        }
    }

    IEnumerator LoadSceneEnum(int index)
    {
        // Sahneyi asenkron bir şekilde çağırıp operasyonu değişkende kaydediyoruz
        AsyncOperation op = SceneManager.LoadSceneAsync(index);

        // Sahnemiz yüklenene kadar sonsuz döngüde bekliyoruz
        while(!op.isDone)
        {
            Debug.Log($"Yükleme {op.progress*100}");
            yield return null;
        }
    }
}
