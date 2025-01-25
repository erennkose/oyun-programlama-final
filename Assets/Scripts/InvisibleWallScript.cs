using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallScript : MonoBehaviour
{
    static ushort flag = 0;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            PlayerScript ps = collider.GetComponent<PlayerScript>();

            if(ps != null)
            {
                ps.spawnPoint = ps.transform.position;

                // Flag 0 ise, Tutorialde oyuncu ölmeyecek şekilde canı azaltılır, burger yenince canın arttığını öğretme amaçlı
                if(flag == 0)
                {
                    if(ps.GetHealth() > 1)
                    {
                        ps.GetDamage();
                    }
                }

                // Flag 2 ise tavşanın kullanımını öğretme amaçlı coin sayısı 50 yapılır.
                else if(flag == 2)
                {
                    ps.coinCount = 50;
                    ps.CoinCollected();
                }   
                flag++;
                Destroy(this.gameObject);
            }
        }
    }
}
