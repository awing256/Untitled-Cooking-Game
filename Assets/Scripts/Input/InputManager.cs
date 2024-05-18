using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager
{
    private PlayerInput playerInput;
    
    public InputManager(PlayerInput playerInput){
        this.playerInput = playerInput;
    } 

    public void SetInputsForPlayer(PlayerController pc)
    {
        pc.SetInputs(playerInput.Player.Primary, playerInput.Player.Secondary, playerInput.Player.Special, 
        playerInput.Player.Utility, playerInput.Player.Ultimate, playerInput.Player.Interact, playerInput.Player.Move,
        playerInput.Player.Aim);        
    }

}
