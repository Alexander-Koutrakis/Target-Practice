
using UnityEngine;
using System.Threading.Tasks;

public static class DissolveShaderController 
{
    //Linearly interpolate fading of material shader
    private static async void Fade(float targetFade,float timeToFade,Material material)
    {
        
        float timer = 0;       
        float fade = material.GetFloat("_Clip");
        float startFade = fade;
        while (fade != targetFade)
        {
            timer += Time.deltaTime / timeToFade;
            fade = Mathf.Lerp(startFade, targetFade, timer);
            material.SetFloat("_Clip", fade);
            await Task.Yield();
        }
    }

    public static void FadeIn(float timeTofade,GameObject gameObject)
    {
        Renderer[] meshRenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in meshRenderers)
        {         
            Material material = renderer.material;
            material.SetFloat("_Clip", 1);
            Fade(0, timeTofade, material);
        }
        
    }

    public static void FadeOut(float timeToFade, GameObject gameObject)
    {
        Renderer[] meshRenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in meshRenderers)
        {
            Material material = renderer.material;
            material.SetFloat("_Clip", 0);
            Fade(1, timeToFade, material);
        }
       
    }
}
