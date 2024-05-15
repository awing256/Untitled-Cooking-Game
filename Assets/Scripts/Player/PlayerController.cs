using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move, dodge, fire, aim;
    [SerializeField] PlayerScriptableObject playerSO; 
    private int curHealth;
    private bool isFiring = false;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject reticle; 
    public WeaponInventoryScript weaponInventory;    
    public enum PlayerState
    {
        Idle,
        Walking,
        Dodging,
        Dead
    }
    public PlayerState currentState;
    public delegate void OnStateChanged(PlayerState newState);
    public event OnStateChanged onStateChanged;
    private PlayerMovement playerMovement;
    private PlayerAim playerAim;
    private PlayerAnimation playerAnimation;
    public float aimAngle;

    public void Initialize(InputAction move, InputAction dodge, InputAction fire, InputAction aim) {
        this.move = move;
        this.move.Enable();
        this.dodge = dodge;
        this.dodge.Enable();
        this.fire = fire;
        this.fire.Enable();
        this.aim = aim;
        this.aim.Enable();

        curHealth = playerSO.maxHealth;
        currentState = PlayerState.Idle;

        playerMovement = new PlayerMovement(playerSO.speed, playerSO.dodgeSpeed, player.GetComponent<Rigidbody2D>(), this);
        playerAim = new PlayerAim(playerSO.aimDistance, player.GetComponent<Transform>(), attackPoint.GetComponent<Transform>(), 
            reticle.GetComponent<Transform>(), this);
        playerAnimation = new PlayerAnimation(player.GetComponent<Animator>(), player.GetComponent<SpriteRenderer>(), 
            attackPoint.GetComponent<SpriteRenderer>(), this );
        
        SubscribeToInputEvents();
        weaponInventory.onWeaponChanged += playerAnimation.OnWeaponChanged;

    }

    private void SubscribeToInputEvents(){
        fire.started += _ => {
            isFiring = true;
        };
        fire.canceled += _ =>{
            isFiring = false;
        };

        dodge.performed += _ => {
            UpdateState(PlayerState.Dodging);
        };
    }


    private void Update(){

        aimAngle = playerAim.DoAim(aim.ReadValue<Vector2>());

        switch (currentState)
        {
            case PlayerState.Idle:
                PlayerActionable();
                break;
            case PlayerState.Walking:
                PlayerActionable();
                break;
            case PlayerState.Dodging:
                playerMovement.DoDodge();
                isFiring = false;
                break;
            case PlayerState.Dead:
                break;
        }
    }

    private void PlayerActionable(){
        playerMovement.DoMove(move.ReadValue<Vector2>());
        playerAnimation.CheckFlip(aimAngle);
        
        if(isFiring){
            DoAttack();
        }
    }


    public void UpdateState(PlayerState newState){
        if (newState == currentState){
            return;
        }
        currentState = newState;

        // Trigger the event, if there are any subscribers
        onStateChanged?.Invoke(currentState);
    }

    private void DoAttack(){
        if (currentState != PlayerState.Dodging){
            weaponInventory.UseWeapon();
        }
    }

    public void TakeDamage(int amount){
        curHealth -= amount;
        if (curHealth <= 0){
            DoDeath();
        } 
        Debug.Log("Health: " + curHealth);
    }

    public void DoDeath(){
        Debug.Log("You are dead");
        UpdateState(PlayerState.Dead);
    }
    



}
