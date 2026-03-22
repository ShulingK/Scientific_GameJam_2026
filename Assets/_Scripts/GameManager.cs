using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Round _round;
    [SerializeField] List<BadEmotion> _badEmotion;
    [SerializeField] public Level _level;
    [SerializeField] public SceneLoader _sceneLoader;

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
        SetActiveBadEmotions(_badEmotion);
    }

    #region Sound

    public void ButtonSound()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance._clickButton);
    }
    #endregion


    #region Rounds
    Round _currentRound;
    public void SetActiveRound(Round round) => _currentRound = round;
    public Round GetActiveRound() => _currentRound;

    public Action OnSuccess; 

    private enum litleEmotion
    {
        JOIE, 
        COLERE,
        PEUR,
        TRISTESSE
    }


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

            lockDrag = true;

            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Lose");
        }
    }
    #endregion

    #region Menu Pause
    public void Home()
    {
        _sceneLoader.LoadScene(0);
    }

    [SerializeField] GameObject _menu;

    public void Pause(bool isOpen)
    {
        if (isOpen) StartCoroutine(COpenPause());
        else StartCoroutine(CClosePause());
    }


    IEnumerator COpenPause()
    {
        yield return null;

        _menu.SetActive(true);
    }
    IEnumerator CClosePause()
    {
        yield return null;

        _menu.SetActive(false);
    }

    public void Restart()
    {
        _sceneLoader.LoadScene(_level.level);
    }
    
    #endregion

    #region Bad Emotions
    List<BadEmotion> _currentBadEmotions;
    public void SetActiveBadEmotions(List<BadEmotion> badEmotions) => _currentBadEmotions = badEmotions;
    public List<BadEmotion> GetActiveBadEmotions() => _currentBadEmotions;

    public Action<int> OnBadEmotionSuccess;

    void CheckBadEmotions()
    {

        Debug.Log("emotions already placed : " + _emotionAlreadyPlaced.Count);

        for (int i = 0; i < GetActiveBadEmotions().Count; i++)
        {
            if (GetActiveBadEmotions()[i].GetBadEmotions().Count != _emotionAlreadyPlaced.Count)
                continue;

            int temp = 0;

            for (int k = 0; k < _emotionAlreadyPlaced.Count; k++)
            {
                for (int j = 0; j < GetActiveBadEmotions()[i].GetBadEmotions().Count; j++)
                {
                    if (j > GetActiveBadEmotions()[i].GetBadEmotions().Count)
                        continue;

                    if (_emotionAlreadyPlaced[k].emotion == GetActiveBadEmotions()[i].GetBadEmotions()[j].emotion &&
                        _emotionAlreadyPlaced[k].slot == GetActiveBadEmotions()[i].GetBadEmotions()[j].slot)
                    {
                        temp++;
                    }
                }
            }

            if (temp == GetActiveBadEmotions()[i].GetBadEmotions().Count)
            {
                OnBadEmotionSuccess?.Invoke(i);
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
            // verifier s'il y en a pas d�j� 1 ? 
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
