using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement
{
    private float speed, dodgeSpeed;
    private Rigidbody2D rb;
    private PlayerController pc;

    public PlayerMovement(float speed, float dodgeSpeed, Rigidbody2D rb, PlayerController pc){
        this.speed = speed;
        this.dodgeSpeed = dodgeSpeed;
        this.rb = rb;
        this.pc = pc;
    }

    public void DoMove(Vector2 movement){
        if (movement != Vector2.zero){
            pc.UpdateState(PlayerController.PlayerState.Walking);
        }else{
            pc.UpdateState(PlayerController.PlayerState.Idle);
        }
        rb.velocity = movement * speed;
    }

    public void DoDodge(){
        rb.velocity = rb.velocity.normalized * dodgeSpeed;
    }

}
