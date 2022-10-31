
using UnityEngine;

public class MultiMissileWeapon : AutoAimingWeapon
{
    public MultiMissileWeapon(Transform spawntransform) : base(spawntransform)
    {
        GameObject multiRockerPrefab = Resources.Load<GameObject>("GameObjects/Projectiles/MultiMissile");
        WeaponIcon = Resources.Load<Sprite>("Sprites/MultiMissiles");
        projectilePool = new ObjectPool<Projectile>(multiRockerPrefab);
    }
}
