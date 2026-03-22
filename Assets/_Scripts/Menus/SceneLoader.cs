using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private float timeBetweenScenes = 2f;

    [SerializeField] private Animator _animator;

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        if (_animator != null)
            _animator.SetTrigger("Load");

        yield return new WaitForSeconds(timeBetweenScenes);

        SceneManager.LoadScene(index);
    }
}
