using System;
using System.Collections;
using UnityEngine;

public class RabbitScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    PlayerScript ps;
    AudioSource audioSource;

    bool canPlayAudio = true;
    ushort coinPrice = 50;
    float currY;

    void Start()
    {
        currY = transform.position.y;

        ps = player.GetComponent<PlayerScript>();
        if(ps == null) Debug.LogError("Player Script bulunamadı!!!");

        audioSource = this.GetComponent<AudioSource>();
        if(audioSource == null) Debug.LogError("Audio Source bulunamadı!!!");
    }

    void Update()
    {
        // 50 Coinimiz olunca animasyonu aktif eden kod
        if(ps.GetCoins() >= coinPrice)
            RabbitMovement();
        
        if(player.GetComponent<PlayerScript>().GetCoins() >= coinPrice && canPlayAudio)
        {
            audioSource.Play();
            canPlayAudio = false; 
        }
    }

    // Rabbit'in yerdeki animasyon fonksiyonu
    void RabbitMovement()
    {
        // Sinüste negatif kısma düşülmemesi için mutlak değerle alt kısımların yukarı çıkarılması
        float newY = (Math.Abs(Mathf.Sin(Time.time*10))/2 + currY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Rabbit'e temas edince double jump özelliğini aktif eden fonksiyon
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player" && player.GetComponent<PlayerScript>().GetCoins() >= coinPrice)
        {
            ps.SetDoubleJump();
            Destroy(this.gameObject);
        }
    }
}
