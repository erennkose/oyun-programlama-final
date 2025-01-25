using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Bu scripti Sigleton olarak kullanmak için static değişken atıyoruz. 
    public static AudioManager instance;

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
}
