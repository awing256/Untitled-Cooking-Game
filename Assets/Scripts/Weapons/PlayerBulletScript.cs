using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : BulletScript
{


    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")) {
            EnemyScript enemyScript = other.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(1);
            Destroy(this.gameObject);
        }else if (other.gameObject.CompareTag("Wall")){
            Destroy(this.gameObject);
        }
    }

}
