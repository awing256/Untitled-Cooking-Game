using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : BulletScript
{

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")) {
            PlayerScript playerScript = GameObject.FindObjectOfType<PlayerScript>();
            if (playerScript.currentState != PlayerScript.PlayerState.Dodging){
                playerScript.TakeDamage(1);
                Destroy(this.gameObject);
            }
        }else if (other.gameObject.CompareTag("Wall")){
            Destroy(this.gameObject);
        }
    }

}
