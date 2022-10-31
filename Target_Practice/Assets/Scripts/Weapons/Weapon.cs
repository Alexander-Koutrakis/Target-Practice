using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//An abstract weapon class fo all weapons
//Holds a pool for projectiles to throw
public abstract class Weapon 
{
    protected float weaponRange = 300f;
    protected ObjectPool<Projectile> projectilePool;
    protected Transform spawnTransform;
    public Sprite WeaponIcon { protected set; get; }
    public Weapon(Transform spawntransform)
    {
        this.spawnTransform = spawntransform;
    }

    protected virtual void ThrowProjectile(Vector3 targetPosition)
    {
        Projectile projectile = projectilePool.GetObject();
        projectile.transform.position = spawnTransform.position;
        projectile.SetTarget(targetPosition);
        projectile.gameObject.SetActive(true);
    }

   
    public abstract void Update();

    public abstract void OnWeaponSelect();

    public abstract void OnWeaponDeselect();
    protected bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
