using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySO", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public float bulletSpeed = 20f;
    public float attackInterval = 2f;
    public float sightRange = 10f;
    public int maxHealth = 3;
    
}