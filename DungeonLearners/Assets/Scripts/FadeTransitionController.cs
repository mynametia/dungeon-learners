using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransitionController : MonoBehaviour
{
    public Animator fadeAnimator;

    private static string sceneToLoad;

    // Triggers fade to black animation
    public void FadeToBlack(string sceneName)
    {
        sceneToLoad = sceneName;
        fadeAnimator.SetTrigger("FadeOut");
    }

    // Event that triggers after scene has fade to black
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
