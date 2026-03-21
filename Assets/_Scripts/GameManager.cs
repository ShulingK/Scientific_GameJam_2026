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

    public Action OnSuccess; 

    void CheckSuccessRound()
    {
        if (GetActiveRound().GetRounds().Count != _emotionAlreadyPlaced.Count)
            return;

        List<SRound> _tempRound = new List<SRound>();
        _tempRound = GetActiveRound().GetRounds();


        for (int i = 0; i < _emotionAlreadyPlaced.Count; i++)
        {
            for (int j = 0; j < _tempRound.Count; j++)
            {
                if (j > _tempRound.Count)
                    continue;

                if (_emotionAlreadyPlaced[i].emotion == _tempRound[j].emotion &&
                    _emotionAlreadyPlaced[i].slot == _tempRound[j].slot)
                {
                    _tempRound.RemoveAt(j);
                }
            }
        }

        if (_tempRound.Count == 0)
            OnSuccess?.Invoke();
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
