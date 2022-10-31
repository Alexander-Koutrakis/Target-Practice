
using UnityEngine;

public class InstantDeathArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damageable"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.TakeDamage(999999);
        }
    }
}
