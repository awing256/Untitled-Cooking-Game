using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    [SerializeField] EnemyScriptableObject enemySO; 
    
    private PlayerScript playerScript;
    private int curHealth;

    private Ray2D ray; 
    private Vector3 direction; 
    private float distance; 

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool canAttack = true;
    
    public enum EnemyState    {
        Idle,
        Notice,
        Chasing,
        Searching,
        Dead, 
        Alert
    }
    public EnemyState currentState;
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        currentState = EnemyState.Idle;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();

        curHealth = enemySO.maxHealth;
    }
    
    void Update(){
        switch(currentState){
            case EnemyState.Idle:
                CheckLOS();
                break;
            case EnemyState.Notice:
                break;
            case EnemyState.Alert:
                CheckLOS();
                DoAttack();
                break;
            case EnemyState.Dead:
                break;
        }

        spriteRenderer.flipX = (player.transform.position.x > this.transform.position.x);
    }

    public void TakeDamage(int amount){
        curHealth -= amount;
        if (curHealth <= 0){
            DoDeath();
        } 
        Debug.Log("Enemy health: " + curHealth);
    }

    public void DoDeath(){
        UpdateState(EnemyState.Dead);
        Destroy(this.gameObject);
    }

    private void DoAttack(){
        if(canAttack && playerScript.currentState != PlayerController.PlayerState.Dead){
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            bullet.transform.right = direction;
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * enemySO.bulletSpeed;
            StartCoroutine(ShotDelay());
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player") && playerScript.currentState != PlayerController.PlayerState.Dodging)
        {
            Debug.Log("Collision enter: " + collision.gameObject.name);
            playerScript.TakeDamage(1);
        }
    }


    private void CheckLOS(){
        direction = player.transform.position - transform.position;
        distance = direction.magnitude;
        ray = new Ray2D(transform.position, direction.normalized);

        Debug.DrawRay(ray.origin, ray.direction * enemySO.sightRange, Color.red);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, enemySO.sightRange);



        foreach (RaycastHit2D hit in hits){
            if (hit.collider.CompareTag("Wall")) {
                UpdateState(EnemyState.Idle);
                return;
            }else if(hit.collider.CompareTag("Player")) {
                UpdateState(EnemyState.Alert);
                return;
            }
        }

        //if player walks out of sight range 
        if (currentState == EnemyState.Alert && distance > enemySO.sightRange ){
            UpdateState(EnemyState.Idle);
        }
    }

    private void UpdateState(EnemyState newState){
        if (currentState == newState){
            return;
        }

        if (newState == EnemyState.Alert && currentState == EnemyState.Idle){
            animator.Play("Notice");
        }else{
            animator.Play(newState.ToString());
        }
        
        currentState = newState;
        
        
    }

    IEnumerator ShotDelay() {
        canAttack = false;
        yield return new WaitForSeconds(enemySO.attackInterval);
        canAttack = true;
    }
}
