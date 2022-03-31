using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls scene change.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public static Stack<string> previousScene = new Stack<string>();
    /// <summary>
    /// Move to scene through sceneID
    /// </summary>
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    /// <summary>
    /// Move to scene through sceneName
    /// </summary>
    public static void MoveToScene(string sceneName)
    {
        previousScene.Push(SceneManager.GetActiveScene().name);
        Debug.Log(previousScene.Peek() +" pushed");
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Return to previous scene
    /// </summary>
    public static void ReturnPreviousScene()
    {
        Debug.Log("Go back to "+previousScene.Peek());
        SceneManager.LoadScene(previousScene.Pop());
    }
}
