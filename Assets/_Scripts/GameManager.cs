using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Awake()
    {
        if (_placementEventChannel != null)
            SubscribePlacementEventChannel();
    }

    #region Rounds
    Round _currentRound;
    public void SetActiveRound(Round round) => _currentRound = round;
    public Round GetActiveRound() => _currentRound;

    void CheckSuccessRound()
    {
        List<SRound> _tempRound = new List<SRound>();
        _tempRound = GetActiveRound().GetRounds();

        foreach(EmotionPlacement emotions in _emotionAlreadyPlaced)
        {
            if (emotion)
        }
    }

    #endregion

    #region Placement Event
    [SerializeField] PlacementEventChannel _placementEventChannel;
    
    private struct EmotionPlacement
    {
        public Emotion emotion;
        public PlayerSlot slot;
    }

    List<EmotionPlacement> _emotionAlreadyPlaced = new List<EmotionPlacement>();

    public void SubscribePlacementEventChannel()
    {
        _placementEventChannel.OnEmotionPlaced += OnEmotionPlaced;
    }

    public void UnsubscribePlacementEventChannel()
    {
        _placementEventChannel.OnEmotionPlaced -= OnEmotionPlaced;
    }

    private void OnEmotionPlaced(Emotion emotion, PlayerSlot slot, bool isDropped)
    {
        EmotionPlacement item;
        item.emotion = emotion;
        item.slot = slot;

        if (isDropped)
        {
            // verifier s'il y en a pas déjà 1 ? 
            _emotionAlreadyPlaced.Add(item);
        }
        else
        {
            for(int i = 0; i < _emotionAlreadyPlaced.Count; i++)
            {
                if (_emotionAlreadyPlaced[i].slot == slot && _emotionAlreadyPlaced[i].emotion == emotion)
                {
                    _emotionAlreadyPlaced.Remove(_emotionAlreadyPlaced[i]);
                    break;
                }
            }
        }

        CheckSuccessRound();
    }
    #endregion
}
