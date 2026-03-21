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
        transform.position = parentBoardSlotTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ReturnToParent();
        //GetComponent<Image>().maskable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out Vector3 globabPointerPosition);
        transform.position = globabPointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().maskable = true;

        if (!transform.parent.CompareTag("PlayerLayer"))
        {
            transform.position = parentBoardSlotTransform.position;
        }
    }

    public Sprite GetObjectSprite()
    {
        return GetComponentInChildren<Image>().sprite;
    }
}
