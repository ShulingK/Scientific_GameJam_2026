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
        yield return new WaitForSeconds(timeBetweenScenes);

        _animator.SetTrigger("Load");

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float duration = stateInfo.length;

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(index);
    }
}
