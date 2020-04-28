using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{

    private AsyncOperation sceneAsync;//Asynchronous operation as we transition from scene to scene


    public void GoToScene(string sceneName, List<GameObject> objectsToMove)//(scene name, Object we want on the next scene)
    {
        StartCoroutine(LoadScene(sceneName, objectsToMove));
    }


    private IEnumerator LoadScene(string sceneName, List<GameObject> objectsToMove)
    {
        SceneManager.LoadSceneAsync(sceneName);

        //Detecting when the new scene is loaded and making it active for the app, done anonymously
        SceneManager.sceneLoaded += (newScene, mode) =>
        {
            SceneManager.SetActiveScene(newScene);
        };

        //Defining the next scene to load
        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);

        //Add some object to the scene while it's loading
        foreach(GameObject obj in objectsToMove){
            SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
        }

        yield return null;
    }
}
