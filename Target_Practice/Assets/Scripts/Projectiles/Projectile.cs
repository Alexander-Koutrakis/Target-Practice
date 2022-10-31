using UnityEngine;


//Projectile moves directly towards target position
public class Projectile : MonoBehaviour, IPoolObject
{

    [SerializeField]protected float speed=3;
    protected Vector3 target;
    private MeshRenderer meshRenderer;
    private Collider projectileCollider;

    //Disable gameobject when target is reached
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damageable")|| other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }   

    private void EnableProjectile()
    {
        meshRenderer.enabled = true;
        projectileCollider.enabled = true;
    }

    protected virtual void Update()
    {
        Move();
    }

    //move directly towards target
    protected virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.LookAt(target);
    }

    private void OnEnable()
    {
        EnableProjectile();
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
   

    public virtual void Initialize()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        projectileCollider = GetComponent<Collider>();
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}

