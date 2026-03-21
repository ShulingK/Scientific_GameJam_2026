using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDroppableSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerSlot slotID;

    private Emotion stockedEmotion = Emotion.None;

    public PlacementEventChannel placementEvent;

    private void Awake()
    {
        this.tag = "PlayerLayer";
    }

    private void Update()
    {
        if (transform.childCount == 0 && stockedEmotion != Emotion.None)
        {
            Debug.Log(stockedEmotion.ToString() + " enlevé de " + slotID.ToString());
            placementEvent.RaiseEvent(stockedEmotion, slotID, false);

            stockedEmotion = Emotion.None;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Slot OnDrop");
        var emotionObject = eventData.pointerDrag.GetComponent<DraggableObject>();

        if (emotionObject != null)
        {

            emotionObject.transform.SetParent(transform);
            emotionObject.transform.position = transform.position;

            Debug.Log(emotionObject.GetEmotionID().ToString() + " placé sur " + slotID.ToString());
            stockedEmotion = emotionObject.GetEmotionID();
            placementEvent.RaiseEvent(stockedEmotion, slotID, true);
        }
    }

}
