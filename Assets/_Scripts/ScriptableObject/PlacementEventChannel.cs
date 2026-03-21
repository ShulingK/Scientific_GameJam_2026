using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Placement Event")]
public class PlacementEventChannel : ScriptableObject
{
    public Action<Emotion, PlayerSlot, bool > OnEmotionPlaced;

    public void RaiseEvent(Emotion emotionID, PlayerSlot playerSlotID, bool isDropped)
    {
        OnEmotionPlaced?.Invoke(emotionID, playerSlotID, isDropped);
    }
}