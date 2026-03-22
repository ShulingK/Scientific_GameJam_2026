using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Emotions")]
    [field: SerializeField] public EventReference _takeEmotion { get; private set; }
    [field: SerializeField] public EventReference _dropEmotion { get; private set; }

    [field: SerializeField] public EventReference _takeJoie { get; private set; }
    [field: SerializeField] public EventReference _dropJoie { get; private set; }
    [field: SerializeField] public EventReference _takeColere { get; private set; }
    [field: SerializeField] public EventReference _dropColere { get; private set; }
    [field: SerializeField] public EventReference _takeTristesse { get; private set; }
    [field: SerializeField] public EventReference _dropTristesse { get; private set; }

    [field: SerializeField] public EventReference _finScene { get; private set; }


    [field: Header("UI")]
    [field: SerializeField] public EventReference _clickButton { get; private set; }

    [field: Header("Musics")]
    [field: SerializeField] public EventReference _mainMenu { get; private set; }

    [field: Header("Narration")]
    [field: SerializeField] public EventReference _sceneEnter { get; private set; }
    [field: SerializeField] public EventReference _goodEmotion { get; private set; }
    [field: SerializeField] public List<EventReference> _badEmotion { get; private set; }
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
