
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private CanvasGroup fadeCanvas;

    private void Awake()
    {
        fadeCanvas = GetComponent<CanvasGroup>();

    }

    private void Start()
    {
        FadeIn(3);
    }
    private async Task Fade(float targetAlpha,float timeToFade)
    {
        float timer = 0;
        float fade = fadeCanvas.alpha;
        float startFade = fade;
        while (fade != targetAlpha)
        {
            timer += Time.deltaTime / timeToFade;
            fade = Mathf.Lerp(startFade, targetAlpha, timer);
            fadeCanvas.alpha = fade;
            await Task.Yield();
        }
    }

    private async Task FadeIn(float timeToFade)
    {
        fadeCanvas.alpha = 1;
        await Fade(0, timeToFade);
    }

    private async Task FadeOut(float timeToFade)
    {
        fadeCanvas.alpha = 0;
        await Fade(1, timeToFade);
    }

    public async void LoadToScene(string sceneName)
    {
        await FadeOut(1);
        SceneManager.LoadScene(sceneName);
    }
}
