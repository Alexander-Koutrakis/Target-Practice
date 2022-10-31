using UnityEngine;

//Spawn targets at random position within range
public class SpawnTargets : MonoBehaviour
{
    private ObjectPool<Target> targetPool;

    //create a pool of targets
    private void Initialize()
    {
        GameObject target = Resources.Load<GameObject>("GameObjects/Target");
        targetPool = new ObjectPool<Target>(target);
    }

    //spawn a target at random position within 10 unit sphere
    public void SpawnTarget()
    {
        
        Target newTarget = targetPool.GetObject();

        Vector3 randomPosition = Random.insideUnitSphere * 10;
        randomPosition = randomPosition + transform.position;
        newTarget.transform.position = randomPosition;
        newTarget.ResetTarget();

        newTarget.gameObject.SetActive(true);

        //manifest effect of target
        DissolveShaderController.FadeIn(2, newTarget.gameObject);
    }

    private void Awake()
    {
        Initialize();
    }

    //spawn 4 random targets
    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            SpawnTarget();
        }
    }
}
