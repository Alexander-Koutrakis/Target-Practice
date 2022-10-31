
using UnityEngine;
using System.Threading.Tasks;

//Get all targets within range infron of the camera and throw homing missiles agains them
public class AutoAimingWeapon : Weapon
{
    
    private RaycastHit[] hits = new RaycastHit[20];

    //amount of targets found
    private int targetamount;

    //checking if firing
    private bool isFiring;

    //an object pool o targetMark visual effect
    private ObjectPool<TargetMark> markPool;

    private LayerMask whatToHit;
   
    
    public AutoAimingWeapon(Transform spawntransform) : base(spawntransform)
    {
        GameObject markPrefab = Resources.Load<GameObject>("GameObjects/TargetMark");
        markPool = new ObjectPool<TargetMark>(markPrefab);
        whatToHit |= 1 << LayerMask.NameToLayer("Damageable");
    }

    //Cast a box infront of the camera to find targets
    private void Target()
    {
        Vector3 scale = new Vector3(20, 10, 40);
        targetamount = Physics.BoxCastNonAlloc(Camera.main.transform.position, scale, Vector3.forward,hits, spawnTransform.rotation,weaponRange, whatToHit);
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            Fire();
        }
    }

    //Throw homing projectiles to each target with a dealy 
    private async Task FireTargets()
    {
        int projectilesFired = 0;
        while (projectilesFired < targetamount)
        {
            ThrowHomingProjectile(hits[projectilesFired].collider.transform);
            projectilesFired++;
            var randomWait = Random.Range(100, 300);            
            await Task.Delay(randomWait);
        }
    }

    //Throw projectile to target
    private void ThrowHomingProjectile(Transform targetTransform)
    {
        HomingProjectile projectile =(HomingProjectile) projectilePool.GetObject();
        projectile.transform.position = spawnTransform.position;
        projectile.transform.rotation = Random.rotation;
        projectile.SetTarget(targetTransform);
        projectile.gameObject.SetActive(true);
    }

    //mark each target with a small dealy
    private async Task MarkTargets()
    {
        int projectilesFired = 0;
        while (projectilesFired < targetamount)
        {
            TargetMark targetMark = markPool.GetObject();
            Transform targetTransform = hits[projectilesFired].collider.transform;
            targetMark.SetTarget(targetTransform);
            targetMark.gameObject.SetActive(true);

            projectilesFired++;
            var randomWait = Random.Range(100, 300);
            await Task.Delay(randomWait);
        }
    }

    //if Not already firing mark all targets and them fire projectiles
    private async void Fire()
    {
        if (!isFiring)
        {
            isFiring = true;
            Target();
            await MarkTargets();
            await FireTargets();
            isFiring = false;
        }
    }

    public override void OnWeaponSelect()
    {
        
    }

    public override void OnWeaponDeselect()
    {
        
    }
}
