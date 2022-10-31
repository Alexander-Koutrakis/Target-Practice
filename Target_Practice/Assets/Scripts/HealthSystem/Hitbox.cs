using UnityEngine;

// Hitbox of the gameobject. Attack here to deal damage to health system
public class Hitbox : MonoBehaviour,IDamageable
{
    private HealthSystem healthSystem;

    public void TakeDamage(float amount)
    {
        healthSystem.TakeDamage(amount);
    }

    private void Awake()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
    }
}
