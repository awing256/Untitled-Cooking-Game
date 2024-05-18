using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstanceScript : MonoBehaviour
{

    private MeleeWeaponClass weaponClass;
    private Quaternion startRotation ;
    private Quaternion targetRotation;
    private float elapsedTime = 0f;
    private float t= 0f;

    public void Initialize(MeleeWeaponClass weaponClass){
        this.weaponClass = weaponClass;
        // startRotation = Quaternion.Euler(0,0,playerController.aimAngle + weaponClass.swingAmount);
        // targetRotation = Quaternion.Euler(0,0,playerController.aimAngle - weaponClass.swingAmount);
    }

    void Update(){

        if (elapsedTime < weaponClass.attackInterval){   
                     
            elapsedTime += Time.deltaTime;
            t = Mathf.Clamp01(elapsedTime / weaponClass.attackInterval);
            this.transform.rotation = Quaternion.LerpUnclamped(startRotation, targetRotation, t);

        }else {
            Destroy(this.gameObject);
        }          
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")) {
            EnemyScript enemyScript = other.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(1);
            Destroy(this.gameObject);
        }else if (other.gameObject.CompareTag("Wall")){
            Destroy(this.gameObject);
        }else if (other.gameObject.CompareTag("EnemyBullet")){
            Destroy(other.gameObject);
        }
    }

}
