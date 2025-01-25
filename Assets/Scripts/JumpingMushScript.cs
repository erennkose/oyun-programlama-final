using UnityEngine;

public class JumpingMushScript : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Mushroom'a dokunulunca y hızını değiştiren fonksiyon
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            PlayerScript ps = collider.GetComponent<PlayerScript>();

            audioSource.Play();

            if(ps != null)
                ps.SetYSpeed(30);
        }
    }
}
