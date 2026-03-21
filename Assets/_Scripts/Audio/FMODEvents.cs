using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Emotions")]
    [field: SerializeField] public EventReference _takeEmotion { get; private set; }
    [field: SerializeField] public EventReference _dropEmotion { get; private set; }


    [field: Header("UI")]
    [field: SerializeField] public EventReference _clickButton { get; private set; }

    [field: Header("Musics")]
    [field: SerializeField] public EventReference _mainMenu { get; private set; }

    public static FMODEvents Instance { get; private set;}

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple FMODEvents instances");
        }
        Instance = this;
    }

}
