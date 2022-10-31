
using UnityEngine;

//Create an explosion to deal damage, shake the camera and push targets away
public class Explosion : MonoBehaviour
{
    
    [SerializeField] private float radius;
    [SerializeField] private float power;
    [SerializeField] private float damage;
    [SerializeField] private string particlesToSpawn;

    //Set how much the camera is going to shake
    [SerializeField] private CameraShakeData cameraShakeData;

    private LayerMask whatTohit;
    private Collider[] colliders;


    private void Awake()
    {
        colliders = new Collider[20];
        whatTohit |= 1 << LayerMask.NameToLayer("Damageable");
    }

    

    public void Explode()
    {
        int amount = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, whatTohit);
        for(int i = 0; i < amount; i++)
        {

            //push away every rigid body inside the sphere
            Rigidbody rigidbody = colliders[i].GetComponentInParent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(power, transform.position, radius);
            }

            //Deal damage to each damageable inside sphere
            IDamageable damageable = colliders[i].GetComponent<IDamageable>();
            damageable.TakeDamage(damage);                       
        }

        //spawn particle effect
        ParticleSystemPoolController.Instance.SpawParticleSystem(particlesToSpawn, transform.position);

        //shake the camera
        CameraShake.Instance.Shake(cameraShakeData);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damageable") || other.CompareTag("Ground"))
        {
            Explode();
        }
    }
}
