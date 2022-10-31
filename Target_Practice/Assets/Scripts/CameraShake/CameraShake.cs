using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    // Amount of Shake
    private Vector3 amount = new Vector3(1f, 1f, 0);
    // Duration of Shake
    private float duration = 1;
    // Shake Speed
    private float speed = 10;
    /// Amount over Lifetime [0,1]
    private AnimationCurve curve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    // <summary>
    // Set it to true: The camera position is set in reference to the old position of the camera
    // Set it to false: The camera position is set in absolute values or is fixed to an object
    // </summary>
    private bool deltaMovement = true;
    private Camera Camera;
    private float time = 0;
    private Vector3 lastPos;
    private Vector3 nextPos;
    private float lastFoV;
    private float nextFoV;

    public static CameraShake Instance;
    private void Awake()
    {
        Camera = Camera.main;
        Instance = this;
    }

    // Do the shake
    public void Shake(CameraShakeData cameraShakeData)
    {
        amount = cameraShakeData.Amount;
        duration = cameraShakeData.Duration;
        speed = cameraShakeData.Speed;
        ResetCam();
        time = duration;
    }

    private void LateUpdate()
    {
        if (time > 0)
        {
            //do something
            time -= Time.deltaTime;
            //next position based on perlin noise
            Shaking(time);  
            if(time<=0)
            {
                //last frame
                ResetCam();               
            }
        }
    }

    private void Shaking(float time)
    {
        nextPos = (Mathf.PerlinNoise(time * speed, time * speed * 2) - 0.5f) * amount.x * transform.right * curve.Evaluate(1f - time / duration) +
                          (Mathf.PerlinNoise(time * speed * 2, time * speed) - 0.5f) * amount.y * transform.up * curve.Evaluate(1f - time / duration);
        nextFoV = (Mathf.PerlinNoise(time * speed * 2, time * speed * 2) - 0.5f) * amount.z * curve.Evaluate(1f - time / duration);

        Camera.fieldOfView += (nextFoV - lastFoV);
        Camera.transform.Translate(deltaMovement ? (nextPos - lastPos) : nextPos);

        lastPos = nextPos;
        lastFoV = nextFoV;
    }

    private void ResetCam()
    {
        //reset the last delta
        Camera.transform.Translate(deltaMovement ? -lastPos : Vector3.zero);
        Camera.fieldOfView -= lastFoV;

        //clear values
        lastPos = nextPos = Vector3.zero;
        lastFoV = nextFoV = 0f;
    }
}