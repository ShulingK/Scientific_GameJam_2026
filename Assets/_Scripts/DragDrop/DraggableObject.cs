using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Emotion emotionID;
    [SerializeField] private Transform parentBoardSlotTransform;

    private RectTransform draggingPlane;


    private void Awake()
    {
        draggingPlane = GetComponent<RectTransform>();
    }
    public Emotion GetEmotionID() => emotionID;

    public void ReturnToParent()
    {
        transform.SetParent(parentBoardSlotTransform);
        //transform.position = parentBoardSlotTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.lockDrag) return;

        ReturnToParent();
        GetComponent<Image>().maskable = false;
        GetComponent<Image>().raycastTarget = false;

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance._takeEmotion);
        // transform.SetParent(panel);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.lockDrag) return;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out Vector3 globabPointerPosition);
        transform.position = globabPointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.lockDrag) return;

        GetComponent<Image>().raycastTarget = true;
        GetComponent<Image>().maskable = false;

        if (!transform.parent.CompareTag("PlayerLayer"))
        {
            ReturnToParent();
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentBoardSlotTransform as RectTransform);
        }
    }

    public Sprite GetObjectSprite()
    {
        return GetComponentInChildren<Image>().sprite;
    }
}
