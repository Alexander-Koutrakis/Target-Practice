
using UnityEngine;

public class NukeWeapon : Weapon
{
    private GameObject GroundTargetGo;
    private string particlesToSpawn = "GroundTarget";
    private LayerMask whatToHit;
    public NukeWeapon(Transform spawntransform) : base(spawntransform)
    {
        GameObject nuke = Resources.Load<GameObject>("GameObjects/Projectiles/Nuke");
        whatToHit |= 1 << LayerMask.NameToLayer("Damageable");
        whatToHit |= 1 << LayerMask.NameToLayer("Ground");
        WeaponIcon = Resources.Load<Sprite>("Sprites/Nuke");
        projectilePool = new ObjectPool<Projectile>(nuke);
        SetGroundTarget();
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            Fire();
        }
        MoveGroundTarget();
    }

    protected void Fire()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponRange, whatToHit))
        {
            Vector3 target = hit.point;
            ThrowProjectile(target);
            ParticleSystemPoolController.Instance.SpawParticleSystem(particlesToSpawn, target);
        }

    }
    private void SetGroundTarget()
    {
        GameObject groundTargetPrefab = Resources.Load<GameObject>("ParticleSystems/GroundTarget");
        GroundTargetGo = GameObject.Instantiate(groundTargetPrefab);
        GroundTargetGo.SetActive(false);

        ParticleSystem[] particleSystems = GroundTargetGo.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem ps in particleSystems)
        {
            var mainModule = ps.main;
            mainModule.loop = true;
        }

        ParticleSystemPoolingObject particleSystemPoolingObject = GroundTargetGo.GetComponent<ParticleSystemPoolingObject>();
        particleSystemPoolingObject.enabled = false;
    }
    private void MoveGroundTarget()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponRange, whatToHit))
        {
            Vector3 target = hit.point;
            GroundTargetGo.transform.position = target;
        }
    }

    protected override void ThrowProjectile(Vector3 targetPosition)
    {
        Projectile projectile = projectilePool.GetObject();
        Vector3 airPosition = new Vector3(targetPosition.x, targetPosition.y + 50, targetPosition.z);
        projectile.transform.position = airPosition;
        projectile.SetTarget(targetPosition);
        projectile.gameObject.SetActive(true);
    }

    public override void OnWeaponSelect()
    {
        GroundTargetGo.SetActive(true);
    }

    public override void OnWeaponDeselect()
    {
        GroundTargetGo.SetActive(false);
    }

}
