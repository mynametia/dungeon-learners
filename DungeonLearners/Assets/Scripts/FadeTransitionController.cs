using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransitionController : MonoBehaviour
{
    public Animator fadeAnimator;
    //public GameObject Player;
    private static string sceneToLoad;

    // Triggers fade to black animation
    public void FadeToBlack(string sceneName)
    {
        //Player = null;
        sceneToLoad = sceneName;
        fadeAnimator.SetTrigger("FadeOut");
    }

    // Event that triggers after scene has fade to black
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
