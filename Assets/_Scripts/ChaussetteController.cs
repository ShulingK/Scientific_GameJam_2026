using System.Collections;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChaussetteController : MonoBehaviour
{
    private Animator chaussetteAnimator;
    [SerializeField] private GameObject inventaire;
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

        //chaussetteAnimator.SetTrigger("OnEnterScene");

        yield return new WaitForSeconds(onEnterTime);

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

    public void BadEndAnimation()
    {

    }

    public void WinAnimation()
    {

    }

}
