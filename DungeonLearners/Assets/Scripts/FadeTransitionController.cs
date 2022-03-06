using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransitionController : MonoBehaviour
{
    public Animator fadeAnimator;
    public GameObject Player;
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
        // Loads player back to their position in the world before they enter the dungeon
        if (SceneManager.GetActiveScene().name == "DungeonRoom" && sceneToLoad == "OpenWorld")
        {
            SceneManager.LoadScene(sceneToLoad);
            Player = GameObject.FindWithTag("Player");
            Player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerWorldX"), PlayerPrefs.GetFloat("PlayerWorldY"), 0);
        }
        // Loads player back to their position in the dungeon room before battle
        else if (SceneManager.GetActiveScene().name == "CardBattle" && sceneToLoad == "DungeonRoom")
        {
            SceneManager.LoadScene(sceneToLoad);
            Player = GameObject.FindWithTag("Player");
            Player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerDungeonX"), PlayerPrefs.GetFloat("PlayerDungeonY"), 0);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }
}
