using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    CharacterController cc;
    AudioSource audioSource;
    UIManager uiManager;

    [SerializeField]
    GameObject dayLight;

    [SerializeField]
    GameObject canvas;

    float maxWalkingSpeed = 10.0f;
    float maxJumpingSpeed = 15.0f;

    float gravity = -50f;

    float xSpeed = 0;
    float ySpeed = 0;

    int jumpCount;
    bool doubleJumpBonus = false;

    float doubleJumpRate = 0.3f;
    float doubleJumpNext = 0f;

    public ushort coinCount = 0;
    ushort health = 3;

    public Vector3 spawnPoint = new Vector3(-16.5f, 6f, 20f);

    Animator animator;

    void Start()
    {
        Time.timeScale = 1;
        cc = this.GetComponent<CharacterController>();
        audioSource = this.GetComponent<AudioSource>();
        uiManager = canvas.GetComponent<UIManager>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        Movement();

        // Oyuncu yere düşerse spawn noktasına ışınlıyor
        if(transform.position.y < -20)
        {
            transform.position = spawnPoint;
        }

        // Animator için gerekli olan değişkenleri atıyoruz
        animator.SetFloat("xSpeed", xSpeed);
        animator.SetFloat("ySpeed", ySpeed);
        animator.SetBool("isFlying", !cc.isGrounded);
    }
    
    // Player'ın hareket fonksiyonu
    void Movement()
    {
        // xSpeed Atanması
        xSpeed = maxWalkingSpeed * Input.GetAxis("Horizontal");

        // Double Jump Kontrolü
        if(doubleJumpBonus && (Time.time >= doubleJumpNext) && (jumpCount < 2) && Input.GetKey(KeyCode.Space))
        {
            ySpeed = maxJumpingSpeed; 
            jumpCount++;
        }

        // Yere değiyor mu
        if (cc.isGrounded)
        {
            jumpCount = 0;
            ySpeed = Mathf.Max(ySpeed, 0); // Hızı sıfırla ama negatif olmayacak şekilde

            if (Input.GetKey(KeyCode.Space))
            {
                doubleJumpNext = Time.time + doubleJumpRate;
                ySpeed = maxJumpingSpeed;
                jumpCount++;
            }
        }

        // Mantar etkisi sonrası yer çekimi sonucunda mantarın colliderının içine düşmeyi önlemek için minimum hız kontrolü
        if (ySpeed < 0 && !cc.isGrounded)
        {
            ySpeed = Mathf.Max(ySpeed, -30f); // Negatif hız sınırı
        }

        // ySpeed'in gravity'e göre güncellenmesi
        ySpeed += gravity * Time.deltaTime;

        // Hızların uygulanması
        cc.Move(new Vector3(xSpeed, ySpeed, 0) * Time.deltaTime);

        // Sağa ve sola döndüğünde yavaşça dönmesi
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal")*-30, 0);
        transform.localRotation = Quaternion.Euler(0,Mathf.Clamp(transform.localRotation.eulerAngles.y,90,270),0);
    }

    // Coin toplanınca CoinScript içinden çalıştırılacak fonksiyon
    public void CoinCollected()
    {
        coinCount++;
        audioSource.Play();
        uiManager.UpdateCoin(coinCount, doubleJumpBonus);
    }

    // Coin Getter fonksiyonu
    public ushort GetCoins()
    {
        return coinCount;
    }

    // Health Getter fonksiyonu
    public ushort GetHealth()
    {
        return health;
    }

    // Burger yenince BurgerScript içinden çalıştırılacak fonksiyon
    public void BurgerEated()
    {
        health++;
        if(health > 3)
            health = 3;
        uiManager.UpdateHealth(health);
    }

    // Enemy ile temasa geçince EnemyScript içinden çalıştırılacak fonksiyon
    public void GetDamage()
    {
        health--;
        animator.SetTrigger("GetHit");
        if(health <= 0 || health >= 4)
        {
            health = 0;
            uiManager.EnableDeathScreen();
            Time.timeScale = 0;
        }
        uiManager.UpdateHealth(health);
    }

    // Mushroom ile temasa geçince JumpingMushScript içinden çalıştırılacak fonksiyon
    public void SetYSpeed(float y)
    {
        ySpeed = y;
    }
    
    // Rabbit ile temasa geçince RabbitScript içinden çalıştırılacak fonksiyon
    public void SetDoubleJump()
    {
        doubleJumpBonus = true;
        uiManager.UpdateCoin(coinCount,doubleJumpBonus);
    }

    // Candy ile temasa geçince CandyScript içinden çalıştırılacak fonksiyon
    public void SetMaxXSpeed(float x)
    {
        maxWalkingSpeed = x;
        dayLight.GetComponent<DayNightCycleScript>().SetCycle(true);
        StartCoroutine(MakeSpeedNormal());
    }

    // 5 saniye sonra hız bonusunu kapatan fonksiyon
    IEnumerator MakeSpeedNormal()
    {
        yield return new WaitForSeconds(5.0f);
        dayLight.GetComponent<DayNightCycleScript>().SetCycle(false);
        maxWalkingSpeed = 10.0f;
    }
}
