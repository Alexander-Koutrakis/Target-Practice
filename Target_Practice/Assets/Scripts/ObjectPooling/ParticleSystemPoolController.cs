using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a library of pools for particle systems
// and spawn them all from here
public class ParticleSystemPoolController : MonoBehaviour
{
    private Dictionary<string, ObjectPool<ParticleSystemPoolingObject>> particleSystemPools = new Dictionary<string, ObjectPool<ParticleSystemPoolingObject>>();
    public static ParticleSystemPoolController Instance;
    
    //Hash all particle Systems from resources
    private void LoadParticleSystems()
    {
        ParticleSystemPoolingObject[] particleSystemPoolingObjects = Resources.LoadAll<ParticleSystemPoolingObject>("ParticleSystems/");
       foreach(ParticleSystemPoolingObject particleSystemPoolingObject in particleSystemPoolingObjects)
        {
            string particleSystemType = particleSystemPoolingObject.gameObject.name;
            ObjectPool<ParticleSystemPoolingObject> particleSystemPool = new ObjectPool<ParticleSystemPoolingObject>(particleSystemPoolingObject.gameObject);
            particleSystemPools.Add(particleSystemType, particleSystemPool);

        }
    }

    //Spawn particle system of name at position
    public void SpawParticleSystem(string name,Vector3 position)
    {
        ParticleSystemPoolingObject particleSystemPoolingObject = particleSystemPools[name].GetObject();
        particleSystemPoolingObject.transform.position = position;
        particleSystemPoolingObject.gameObject.SetActive(true);
    }

    private void Awake()
    {
        Instance = this;
        LoadParticleSystems();
    }
}
