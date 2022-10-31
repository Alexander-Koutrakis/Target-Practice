
using UnityEngine;

//projectile that always moves forward and slowly turning towards target
public class HomingProjectile : Projectile
{
    private Transform targetTransform;
    private float rotationSpeed=15f;
    protected override void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected override void Update()
    {
        base.Update();
        LookAtTarget(); ;
    }

    public void SetTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    //look at target over time
    private void LookAtTarget()
    {
        Vector3 direction = targetTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
