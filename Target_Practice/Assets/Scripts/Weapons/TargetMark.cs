using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A target effect that marks a target for homming attacks
//It rotates around itself and fades over time
public class TargetMark : MonoBehaviour,IPoolObject
{
    private float timeRemaining;
    private const float LIFETIME = 3;
    private float rotationspeed = 180;
    private Transform target;
    private Image image;
    private Camera mainCamera;
    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;                     
            if (timeRemaining <= 0)
            {
                gameObject.SetActive(false);
            }
        }       
    }


    private void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        image.transform.Rotate(Vector3.forward, rotationspeed * Time.deltaTime);
        float percent =(LIFETIME - timeRemaining) / LIFETIME;
        float alpha = Mathf.Lerp(1, 0, percent);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        timeRemaining = LIFETIME;
        transform.position = target.position;
    }
 

    public void Initialize()
    {
        mainCamera = Camera.main;
        image = GetComponentInChildren<Image>();
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

   
}
