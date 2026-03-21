using System;
using System.Collections.Generic;
using UnityEngine;


// Structure qui défini un Round avec : 
// - le type d'item
// - la partie du corps associée
[Serializable]
public struct SRound
{
    [SerializeField] public int i;
    // ItemType 
    // Body Part
}


[CreateAssetMenu(fileName = "Round", menuName = "Scriptable Objects/Round")]
public class Round : ScriptableObject
{
    [SerializeField]
    List<SRound> _rounds = new List<SRound>();

    public List<SRound> GetRounds() => _rounds;
    
}
