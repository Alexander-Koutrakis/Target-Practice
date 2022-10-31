
using UnityEngine;
using System;
using System.Threading.Tasks;
public class HealthSystem : MonoBehaviour,IResetable
{
    private float currentHealth;
    public float MaxHealth { private set; get; }
    public Action<HealthSystem> onDeath;
    public Action<HealthSystem, float> onTakeDamage;
    public bool IsDead { get { return currentHealth < 0 ? true : false; } }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        onTakeDamage?.Invoke(this,amount);
        DamageNumbersController.Instance.ShowDamage(this, amount);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //Dissolve the mesh on death and disable when the mesh is disolved
    private void Die()
    {
        onDeath?.Invoke(this);
        DissolveShaderController.FadeOut(1,gameObject);
        DisableAfterTime(1);
    }

    public void Reset()
    {
        currentHealth = MaxHealth;
    }

    private async void DisableAfterTime(float timeToDisable)
    {
        float timer = 0;
        while(timer< timeToDisable)
        {
            timer += Time.deltaTime / timeToDisable;
            await Task.Yield();
        }

        gameObject.SetActive(false);
    }

  
  
}
