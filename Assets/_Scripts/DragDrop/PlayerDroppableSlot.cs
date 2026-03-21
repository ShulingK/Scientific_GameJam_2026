using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDroppableSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerSlot slotID;

    //private Emotion stockedEmotion = Emotion.None;
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
            placementEvent.RaiseEvent(stockedEmotion.GetEmotionID(), slotID, false);
        }

        stockedEmotion = eventData.pointerDrag.GetComponent<DraggableObject>();

        stockedEmotion.transform.SetParent(transform);
        stockedEmotion.transform.position = transform.position;

        Debug.Log(stockedEmotion.GetEmotionID() + " placé sur " + slotID);
        placementEvent.RaiseEvent(stockedEmotion.GetEmotionID(), slotID, true);

    }

}
