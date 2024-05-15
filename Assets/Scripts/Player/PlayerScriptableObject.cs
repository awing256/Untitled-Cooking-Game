using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerSO", order = 1)]
public class PlayerScriptableObject : ScriptableObject
{
    public float speed = 3f;
    public float aimDistance = .4f;
    public float dodgeSpeed = 5f;
    public int maxHealth = 3;
    
}