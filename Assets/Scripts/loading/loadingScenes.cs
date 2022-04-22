using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadingScenes : MonoBehaviour
{


    IEnumerator Start()
    {
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(loadScenes());
    }

    IEnumerator loadScenes()
    {

        AsyncOperation loadMainScene = SceneManager.LoadSceneAsync(2);
        while (loadMainScene.progress < 1)
        {
            yield return null;
        }

    }
}
