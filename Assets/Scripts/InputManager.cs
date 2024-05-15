using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerController playerController;
    private CraftingScript craftingScript;
    private WeaponInventoryScript weaponInventoryScript;
    

    void Start(){
        Cursor.visible = false;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerController = GameObject.FindObjectOfType<PlayerController>();
        playerController.Initialize(playerInput.Player.Move, playerInput.Player.Dodge, playerInput.Player.Fire, playerInput.Player.Aim);

        craftingScript = GameObject.FindObjectOfType<CraftingScript>();
        craftingScript.Initialize(playerInput.Player.Craft, playerInput.Player.SelectCraft, playerInput.Player.StartCraft);

        weaponInventoryScript = GameObject.FindObjectOfType<WeaponInventoryScript>();
        weaponInventoryScript.Initialize(playerInput.Player.SelectWeapon);
        
    }

}
