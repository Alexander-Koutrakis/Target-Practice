using UnityEngine;

//Spawn damage numbers from a pool at a specific position
public class DamageNumbersController : MonoBehaviour
{

    private static ObjectPool<DamageNumber> objectPool;
    public static DamageNumbersController Instance;

    public void Awake()
    {
        GameObject damageNumberPrefab = Resources.Load<GameObject>("GameObjects/DamageNumber");
        objectPool = new ObjectPool<DamageNumber>(damageNumberPrefab);
        Instance = this;
    }

    public void ShowDamage(HealthSystem healthSystem,float damage)
    {
        DamageNumber damageNumber = objectPool.GetObject();
        damageNumber.gameObject.SetActive(true);
        damageNumber.ShowDamage(healthSystem, damage);
    }


}
