using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int level;

    public void Next() => level++;

    public void Reset() => level = 1;
}
