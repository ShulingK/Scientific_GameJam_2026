using System;
using System.Collections;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChaussetteController : MonoBehaviour
{
    private Animator chaussetteAnimator;
    [SerializeField] private GameObject inventaire;
    [SerializeField] private GameObject dialogue;
    [SerializeField] float onEnterTime;

    private void Awake()
    {
        chaussetteAnimator = GetComponent<Animator>();

        GameManager.Instance.OnSuccess += OnWinAnimation;
        GameManager.Instance.OnBadEmotionSuccess += OnBadEmotionAnimation;
    }

    private void Start()
    {
        OnEnterSceneAnimation();
    }


    public void OnEnterSceneAnimation()
    {
        StartCoroutine(OnEnterSceneAnimationCoroutine());
    }

    private IEnumerator OnEnterSceneAnimationCoroutine()
    {
        HideInventory();

        ShowDialogue();

        chaussetteAnimator.SetTrigger("OnEnterScene");

        yield return new WaitForSeconds(onEnterTime);

        HideDialogue();

        ShowInventory();

    }

    private void HideInventory()
    {
        inventaire.SetActive(false);
    }

    private void ShowInventory()
    {
        inventaire.SetActive(true);
    }

    private void HideDialogue()
    {
        dialogue.SetActive(false);
    }

    private void ShowDialogue()
    {
        dialogue.SetActive(true);
    }

    private void OnWinAnimation()
    {
        chaussetteAnimator.SetTrigger("OnWin");
    }

    private void OnBadEmotionAnimation(int obj)
    {
        chaussetteAnimator.SetTrigger("OnBadAnimation" + obj);
    }



}
