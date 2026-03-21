using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerPlacementSlot slotID;

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
