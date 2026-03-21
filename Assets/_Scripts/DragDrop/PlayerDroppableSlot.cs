using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDroppableSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerSlot slotID;

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
