using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private float timeBetweenScenes = 2f;
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        yield return new WaitForSeconds(timeBetweenScenes);

        SceneManager.LoadScene(index);
    }
}
