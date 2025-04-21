using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonMono<LoadSceneManager>
{
    public void OnLoadScene(string sceneName, Action<object> callback)
    {
        StartCoroutine(LoadScene(new LoadSceneData
        {
            sceneName = sceneName,
            callback = callback
        }));
    }

    IEnumerator LoadScene(LoadSceneData loadSceneData)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadSceneData.sceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => asyncLoad.isDone);

        if (loadSceneData.callback != null)
        {
            loadSceneData.callback.Invoke(loadSceneData.sceneName + "Done");
        }
    }
}

public class LoadSceneData
{
    public string sceneName;
    public Action<object> callback;
}
