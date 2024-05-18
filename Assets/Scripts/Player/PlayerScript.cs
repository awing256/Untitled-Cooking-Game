using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PlayerController pc;
    public PlayerState currentState;
    private int curHealth;
    public float aimAngle;
    [SerializeField] PlayerScriptableObject playerSO;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject reticle;   

    public enum PlayerState
    {
        Idle,
        Walking,
        Dodging,
        Dead
    }
    public delegate void OnStateChanged(PlayerState newState);
    public event OnStateChanged onStateChanged;

    
    void Awake()
    {
        this.pc = new PlayerController(playerSO.speed, playerSO.dodgeSpeed, playerSO.aimDistance, this, this.attackPoint, this.reticle );
        curHealth = playerSO.maxHealth;        
    }

    void Update(){
        this.pc.Update();
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

    public PlayerController GetPlayerController(){
        return this.pc;
    }

    private void OnExitAnimation(PlayerState endState){
        UpdateState(endState);
    }

    public void UpdateState(PlayerState newState){
        if (newState == currentState){
            return;
        }
        currentState = newState;

        // Trigger the event, if there are any subscribers
        onStateChanged?.Invoke(currentState);
    }

    public void setAimAngle(float newAngle){
        this.aimAngle = newAngle;
    }

}
