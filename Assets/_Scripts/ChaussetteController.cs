using System.Collections;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChaussetteController : MonoBehaviour
{
    private Animator chaussetteAnimator;
    [SerializeField] private GameObject inventaire;

    private void Awake()
    {
        chaussetteAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        //OnEnterSceneAnimation();

        chaussetteAnimator.SetTrigger("OnEnterScene");
    }


    public void OnEnterSceneAnimation()
    {
        StartCoroutine(OnEnterSceneAnimationCoroutine());
    }

    private IEnumerator OnEnterSceneAnimationCoroutine()
    {

        HideInventory();

        chaussetteAnimator.SetTrigger("OnEnterScene");

        while(chaussetteAnimator.GetCurrentAnimatorStateInfo(0).IsName("OnEnterScene"))
        {
            Debug.Log("Still on Enter Scene");
            yield return null;
        }

        ShowInventory();

    }

    private void HideInventory()
    {
        inventaire.SetActive(true);
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
