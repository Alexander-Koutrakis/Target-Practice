
using UnityEngine;

//Reset all Target stats when spawning
public class Target : MonoBehaviour,IPoolObject
{
    private IResetable[] resetables;
    public void Initialize()
    {
        resetables = GetComponentsInChildren<IResetable>();
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void ResetTarget()
    {
        for(int i = 0; i < resetables.Length; i++)
        {
            resetables[i].Reset();
        }
    }
}
