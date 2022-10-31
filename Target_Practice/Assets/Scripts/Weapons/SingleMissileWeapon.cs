
using UnityEngine;

//Throw direct projectiles towords mouse position
//use a line renderer to aim
public class SingleMissileWeapon : Weapon
{
    private LineRenderer lineRenderer;
    private LayerMask whatToHit;
    public SingleMissileWeapon(Transform spawntransform) : base(spawntransform)
    {
        GameObject rocket = Resources.Load<GameObject>("GameObjects/Projectiles/Missile");
        WeaponIcon = Resources.Load<Sprite>("Sprites/SingleMissile");
        projectilePool = new ObjectPool<Projectile>(rocket);
        whatToHit |= 1 << LayerMask.NameToLayer("Damageable");
        whatToHit |= 1 << LayerMask.NameToLayer("Ground");
        
        AddLineRenderer();
    }

    //Add line renderer and its parameters
    private void AddLineRenderer()
    {
        lineRenderer = spawnTransform.gameObject.AddComponent<LineRenderer>();
        Color fadeRed = new Color(1, 0, 0, 0.3f);
        lineRenderer.startColor = fadeRed;
        lineRenderer.endColor = fadeRed;
        
        lineRenderer.SetPosition(0, spawnTransform.position);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.enabled = false;
    }

    //Update line renderer to follow mouse posiiton
    private void UpdateLineRenderer()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        Vector3 endpoint;
        if (Physics.Raycast(ray,out hit,weaponRange, whatToHit))
        {
            endpoint = hit.point;
        }
        else
        {
            endpoint = ray.GetPoint(weaponRange);
        }
        lineRenderer.SetPosition(1,endpoint);

    }

    //Throw projectile
    private void Fire()
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 endPoint = ray.GetPoint(weaponRange);
        ThrowProjectile(endPoint);
    }


    public override void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            Fire();
        }

        UpdateLineRenderer();
    }

    //enable line renderer when weapon is selected
    public override void OnWeaponSelect()
    {
        lineRenderer.enabled = true;
    }

    //disble line renderer when weapon is deselected
    public override void OnWeaponDeselect()
    {
        lineRenderer.enabled = false;
    }
}
