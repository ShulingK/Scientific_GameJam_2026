using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int level;

    private int maxLevel;

    public void Next()
    {
        level++;

        if (level >= SceneManager.sceneCountInBuildSettings) 
            Reset();
    }

    public void Reset() => level = 0;
}
