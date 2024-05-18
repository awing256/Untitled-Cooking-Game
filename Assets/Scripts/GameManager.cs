using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerScript ps;
    InputManager inputManager;
    void Start(){
        Cursor.visible = false;
        this.inputManager = new InputManager(new PlayerInput());
        this.inputManager.SetInputsForPlayer(ps.GetPlayerController());
    }
}
