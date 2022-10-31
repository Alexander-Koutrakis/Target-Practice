
using UnityEngine;

//Disable gameobject when all its particles systems have stoped playing
//use it for object pooling
public class ParticleSystemPoolingObject : MonoBehaviour, IPoolObject
{
    private ParticleSystem[] particleSystems=new ParticleSystem[0];

    //Check if any Particle system is playing
    private bool PSisPlaying()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (particleSystems[i].isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    public void LateUpdate()
    {
        if (!PSisPlaying())
        {
            gameObject.SetActive(false);
            EnableParticlesystems();
        }
    }


    public void EnableParticlesystems()
    {
        for(int i = 0;i< particleSystems.Length; i++)
        {
            particleSystems[i].gameObject.SetActive(true);
        }
    }
 
    public void Initialize()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }  
}

