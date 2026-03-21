using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "BadEmotion", menuName = "Scriptable Objects/BadEmotion")]
public class BadEmotion : ScriptableObject
{
    [SerializeField]
    List<SRound> _badEmotions = new List<SRound>();

    public List<SRound> GetBadEmotions() => _badEmotions;

}
