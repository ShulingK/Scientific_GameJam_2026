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

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance._sceneEnter);

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

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance._goodEmotion);

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


        GameManager.Instance._level.Next();

        Debug.LogWarning("Level To load : " + GameManager.Instance._level.level);

        GameManager.Instance._sceneLoader.LoadScene(GameManager.Instance._level.level);
    }

    private void OnBadEmotionAnimation(int obj)
    {
        chaussetteAnimator.SetTrigger("OnBadAnimation" + obj);

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance._badEmotion[obj]);

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
