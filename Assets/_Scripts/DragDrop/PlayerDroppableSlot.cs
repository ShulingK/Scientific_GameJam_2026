using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerDroppableSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerSlot slotID;

    [SerializeField] private GameObject leftHand;

    private Image leftHandImage;

    private DraggableObject stockedEmotion;

    public PlacementEventChannel placementEvent;

    private void Awake()
    {
        this.tag = "PlayerLayer";
    }

    private void Update()
    {
        if (transform.childCount == 0 && stockedEmotion != null)
        {
            Debug.Log(stockedEmotion.GetEmotionID() + " enlevé de " + slotID);
            DeleteOnLeftHand();
            placementEvent.RaiseEvent(stockedEmotion.GetEmotionID(), slotID, false);
            stockedEmotion = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (stockedEmotion != null)
        {
            stockedEmotion.ReturnToParent();
            Debug.Log(stockedEmotion.GetEmotionID() + " enlevé de " + slotID);
            DeleteOnLeftHand();
            placementEvent.RaiseEvent(stockedEmotion.GetEmotionID(), slotID, false);
        }

        stockedEmotion = eventData.pointerDrag.GetComponent<DraggableObject>();

        stockedEmotion.transform.SetParent(transform);
        stockedEmotion.transform.position = transform.position;

        Debug.Log(stockedEmotion.GetEmotionID() + " placé sur " + slotID);
        DuplicateOnLeftHand();
        placementEvent.RaiseEvent(stockedEmotion.GetEmotionID(), slotID, true);

        stockedEmotion.GetComponent<Image>().raycastTarget = true;
        stockedEmotion.GetComponent<Image>().maskable = true;

        //AudioManager.Instance.PlayOneShot(FMODEvents.Instance._dropEmotion);
    }

    private void DuplicateOnLeftHand()
    {
        if (slotID == PlayerSlot.Main && leftHand != null)
        {
            leftHandImage = leftHand.GetComponent<Image>();
            leftHandImage.sprite = stockedEmotion.GetObjectSprite();
            leftHandImage.preserveAspect = true;
            leftHandImage.enabled = true;
        }
    }

    private void DeleteOnLeftHand()
    {
        if (slotID == PlayerSlot.Main && leftHand != null)
        {
            leftHandImage.enabled = false;
            leftHandImage = null;
        }
    }


}
