using UnityEngine;
using TMPro;

//damage number that spawns above an IDamageable object upon taking damage
public class DamageNumber : MonoBehaviour,IPoolObject
{
    private static int speed = 1;
    private const float LIFETIME = 3;
    private float remainingLifetime=30;
    private float fontSize;
    private Color32 textColor;
    private static Camera mainCamera;
    private TMP_Text tMP_Text;


    private void Awake()
    {
        Initialize();
    }

    //Control the lifetime of the text
    private void Update()
    {
        if (remainingLifetime > 0)
        {
            remainingLifetime -= Time.deltaTime;
            Fade();
            if (remainingLifetime <= 0)
            {
                gameObject.SetActive(false);
            }
        }

    }


    //Move text upwards and alwace face the camera
    private void LateUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }

    //Color and size fade 
    private void Fade()
    {
        float fadepercent = (LIFETIME - remainingLifetime) / LIFETIME;
        float alpha = Mathf.Lerp(textColor.a, 0, fadepercent);
        float fontsize = Mathf.Lerp(fontSize, 28, fadepercent);
        tMP_Text.color = new Color32(textColor.r,textColor.g,textColor.b,(byte)alpha);
        tMP_Text.fontSize = fontsize;
    }

    //Spawn damage number on position
    public void ShowDamage(HealthSystem healthSystem,float damageValue)
    {

        transform.position = healthSystem.transform.position;
        tMP_Text.SetText(damageValue.ToString());
        SetTextSizeColor(healthSystem.MaxHealth,damageValue);

        remainingLifetime = LIFETIME;
        gameObject.SetActive(true);
    }

    //Set Color and Font size according to how much damage was dealt
    private void SetTextSizeColor(float maxHealth, float damage)
    {
        float damagePercent = damage / maxHealth;
        if (damagePercent >= 0.5f)
        {
            textColor = new Color32(255, 0, 0, 255);
            fontSize = 72;
        }
        else if (damagePercent >= 0.25f)
        {
            textColor = new Color32(255, 255, 0, 255);
            fontSize = 54;
        }
        else if (damagePercent >= 0.1f)
        {
            textColor = new Color32(255, 255, 255, 255);
            fontSize = 36;
        }
        else
        {
            textColor = new Color32(92, 92, 92, 255);
            fontSize = 28;
        }

    }

 
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Initialize()
    {
        tMP_Text = GetComponentInChildren<TMP_Text>(true);
        mainCamera = Camera.main;
    }

}
