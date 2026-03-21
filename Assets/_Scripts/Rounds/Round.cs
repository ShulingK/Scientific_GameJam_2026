using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// Structure qui défini un Round avec : 
// - le type d'item
// - la partie du corps associée
[Serializable]
public struct SRound
{
    public Emotion emotion;
    public PlayerSlot slot;
}


[CreateAssetMenu(fileName = "Round", menuName = "Scriptable Objects/Round")]
public class Round : ScriptableObject
{
    [SerializeField]
    List<SRound> _rounds = new List<SRound>();

    public List<SRound> GetRounds() => _rounds;
    
}
