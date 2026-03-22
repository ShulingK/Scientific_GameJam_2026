using System;
using System.Collections.Generic;
//using UnityEditor.VisionOS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Round _round;
    [SerializeField] List<BadEmotion> _badEmotions;
    [SerializeField] Level _level;
    [SerializeField] SceneLoader _sceneLoader;

    [SerializeField] ChaussetteController chaussette;

    public static GameManager Instance { get; private set; }

    public bool lockDrag = false;
    
    public void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found plusieurs AudioManager");
            return;
        }
        Instance = this;

        if (_placementEventChannel != null)
            SubscribePlacementEventChannel();
        SetActiveRound(_round);
        SetActiveBadEmotions(_badEmotions);
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

        int temp = 0;

        for (int i = 0; i < _emotionAlreadyPlaced.Count; i++)
        {
            for (int j = 0; j < GetActiveRound().GetRounds().Count; j++)
            {
                if (j > GetActiveRound().GetRounds().Count)
                    continue;

                if (_emotionAlreadyPlaced[i].emotion == GetActiveRound().GetRounds()[j].emotion &&
                    _emotionAlreadyPlaced[i].slot == GetActiveRound().GetRounds()[j].slot)
                {
                    temp++;
                }
            }
        }

        if (temp == GetActiveRound().GetRounds().Count)
        {
            OnSuccess?.Invoke();

            _level.Next();
            _sceneLoader.LoadScene(_level.level);
            lockDrag = true;

            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Lose");
        }
    }


    #endregion

    #region Bad Emotions
    List<BadEmotion> _currentBadEmotions;
    public void SetActiveBadEmotions(List<BadEmotion> badEmotions) => _currentBadEmotions = badEmotions;
    public List<BadEmotion> GetActiveBadEmotions() => _currentBadEmotions;

    Action OnBadEmotionSuccess;

    void CheckBadEmotions()
    {
        foreach (BadEmotion badEmotion in GetActiveBadEmotions())
        {

            if (badEmotion.GetBadEmotions().Count != _emotionAlreadyPlaced.Count)
                return;

            int temp = 0;

            for (int i = 0; i < _emotionAlreadyPlaced.Count; i++)
            {
                for (int j = 0; j < badEmotion.GetBadEmotions().Count; j++)
                {
                    if (j > badEmotion.GetBadEmotions().Count)
                        continue;

                    if (_emotionAlreadyPlaced[i].emotion == badEmotion.GetBadEmotions()[j].emotion &&
                        _emotionAlreadyPlaced[i].slot == badEmotion.GetBadEmotions()[j].slot)
                    {
                        temp++;
                    }
                }
            }

            if (temp == badEmotion.GetBadEmotions().Count)
            {
                OnBadEmotionSuccess?.Invoke();
                Debug.LogWarning("Bad EMOTION !!!!");
            }
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
                    _emotionAlreadyPlaced.RemoveAt(i);
                    break;
                }
            }
        }

        CheckSuccessRound();
        CheckBadEmotions();
    }
    #endregion
}
