using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static Stack<string> previousScene = new Stack<string>();
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public static void MoveToScene(string sceneName)
    {
        previousScene.Push(SceneManager.GetActiveScene().name);
        Debug.Log(previousScene.Peek() +" pushed");
        SceneManager.LoadScene(sceneName);
    }

    public static void ReturnPreviousScene()
    {
        Debug.Log("Go back to "+previousScene.Peek());
        SceneManager.LoadScene(previousScene.Pop());
    }
}
