using UnityEngine;

public class PlayerHitboxScript : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = this.GetComponentInParent<Animator>();
    }

    // Player'ın Hitbox'ının hasar veren fonksiyonu
    void OnTriggerStay(Collider collider)
    {
        if(Input.GetMouseButton(0))
        {
            animator.SetTrigger("Attack");
            if(collider.tag == "Enemy")
            {
                EnemyScript es = collider.GetComponent<EnemyScript>();

                if(es != null)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
