using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadingScenesSmall : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log("small loadin");

        yield return new WaitForSeconds(3.0f);

        StartCoroutine(loadScenes());
    }

    IEnumerator loadScenes()
    {
        SceneManager.UnloadSceneAsync(2);
        AsyncOperation loadSmallScene = SceneManager.LoadSceneAsync(4);
        while (loadSmallScene.progress < 1)
        {
            yield return null;
        }

    }
}
