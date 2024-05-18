using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
    private PlayerScript ps;
    private PlayerMovement playerMovement;
    private PlayerAim playerAim;
    private PlayerAnimation playerAnimation;
    private GameObject player;
    
    private InputAction primary, secondary, special, utility, ultimate, interact, move, aim;

    

    public PlayerController(float playerSpeed, float dodgeSpeed, float aimDistance, PlayerScript ps, GameObject attackPoint, 
        GameObject reticle) {
        this.ps = ps;
        this.player = ps.gameObject;
        playerMovement = new PlayerMovement(playerSpeed, dodgeSpeed, player.GetComponent<Rigidbody2D>(), ps);
        playerAim = new PlayerAim(aimDistance, player.GetComponent<Transform>(), attackPoint.GetComponent<Transform>(), 
            reticle.GetComponent<Transform>(), ps);
        playerAnimation = new PlayerAnimation(player.GetComponent<Animator>(), player.GetComponent<SpriteRenderer>(), 
            attackPoint.GetComponent<SpriteRenderer>(), ps);

        ps.onStateChanged += OnStateChanged;
    }

    public void SetInputs(InputAction primary, InputAction secondary, InputAction special, InputAction utility, 
    InputAction ultimate, InputAction interact, InputAction move, InputAction aim){
        Debug.Log("entering pc for input set");
        this.move = move;
        this.move.Enable();
        this.primary = primary;
        this.primary.Enable();
        this.secondary = secondary;
        this.secondary.Enable();
        this.special = special;
        this.special.Enable();
        this.utility = utility;
        this.utility.Enable();
        this.ultimate = ultimate;
        this.ultimate.Enable();
        this.interact = interact;
        this.interact.Enable();
        this.move = move;
        this.move.Enable();

        playerAim.SetInput(aim);

        SubscribeToInputEvents();
    }

    private void SubscribeToInputEvents(){
        this.utility.performed += _ => {
            ps.UpdateState(PlayerScript.PlayerState.Dodging);
        };
    }
    private void OnStateChanged(PlayerScript.PlayerState newState){
        
    }


    public void Update(){

        ps.setAimAngle(playerAim.DoAim());

        switch (ps.currentState)
        {
            case PlayerScript.PlayerState.Idle:
                PlayerActionable();
                break;
            case PlayerScript.PlayerState.Walking:
                PlayerActionable();
                break;
            case PlayerScript.PlayerState.Dodging:
                playerMovement.DoDodge();
                break;
            case PlayerScript.PlayerState.Dead:
                break;
        }
    }

    private void PlayerActionable(){
        playerMovement.DoMove(move.ReadValue<Vector2>());
        playerAnimation.CheckFlip(ps.aimAngle);
    }
}
