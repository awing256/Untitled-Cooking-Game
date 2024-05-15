using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public PlayerController.PlayerState currentState;

    void Start()
    {
        // Subscribe to event
        playerController.onStateChanged += OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(PlayerController.PlayerState newState){
        currentState = newState;
    }

    // Unsubscribe from the event when the script is destroyed
    void OnDestroy()
    {
        playerController.onStateChanged -= OnPlayerStateChanged;
    }

    public void TakeDamage(int amount){
        playerController.TakeDamage(amount);
    }

    private void OnExitAnimation(PlayerController.PlayerState endState){
        playerController.UpdateState(endState);
    }

}
