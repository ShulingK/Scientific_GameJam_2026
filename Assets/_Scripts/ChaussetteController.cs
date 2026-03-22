using System.Collections;
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

        GameManager.Instance.OnSuccess += OnWinAnimation;
        GameManager.Instance.OnBadEmotionSuccess += OnBadEmotionAnimation;

        HideDialogue();

        ShowInventory();

    }

    private void HideInventory()
    {
        if (inventaire != null)
            inventaire.SetActive(false);
    }

    private void ShowInventory()
    {
        if (inventaire != null)
            inventaire.SetActive(true);
    }

    private void HideDialogue()
    {
        if (dialogue != null)
            dialogue.SetActive(false);
    }

    private void ShowDialogue()
    {
        if (dialogue != null)
            dialogue.SetActive(true);
    }

    private void OnWinAnimation()
    {
        chaussetteAnimator.SetTrigger("OnWin");

        StartCoroutine(OnWinCoroutine());
    }

    IEnumerator OnWinCoroutine()
    {
        HideInventory();

        ShowDialogue();

        yield return new WaitForSeconds(0.5f);

        AnimatorStateInfo stateInfo = chaussetteAnimator.GetCurrentAnimatorStateInfo(0);
        float duration = stateInfo.length;

        Debug.Log("Animation Duration : " + duration);

        yield return new WaitForSeconds(duration);

        HideDialogue();

        ShowInventory();
    }

    private void OnBadEmotionAnimation(int obj)
    {
        chaussetteAnimator.SetTrigger("OnBadAnimation" + obj);

        StartCoroutine(OnBadEmotionAnimation());
    }


    IEnumerator OnBadEmotionAnimation()
    {
        HideInventory();

        ShowDialogue();

        yield return new WaitForSeconds(0.5f);

        AnimatorStateInfo stateInfo = chaussetteAnimator.GetCurrentAnimatorStateInfo(0);
        float duration = stateInfo.length;

        Debug.Log("Animation Duration : " + duration);

        yield return new WaitForSeconds(duration);


        HideDialogue();

        ShowInventory();
    }


}
