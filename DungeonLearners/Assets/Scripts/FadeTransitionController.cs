using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransitionController : MonoBehaviour
{
    public Animator fadeAnimator;
    //public GameObject Player;
    private static string sceneToLoad;
    private static Scene lastScene;

    public void ReturnToPreviousScene()
    {
        FadeToBlack(lastScene.name);
    }

    // Triggers fade to black animation
    public void FadeToBlack(string sceneName)
    {
        //Player = null;
        lastScene = SceneManager.GetActiveScene();
        sceneToLoad = sceneName;
        fadeAnimator.SetTrigger("FadeOut");
    }

    // Event that triggers after scene has fade to black
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
