using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found plusieurs AudioManager");
        }
        Instance = this;

        _masterBus = RuntimeManager.GetBus("bus:/");
        _musicBus = RuntimeManager.GetBus("bus:/Musique");
        _ambienceBus = RuntimeManager.GetBus("bus:/Ambiance");
        _SFXBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        _masterBus.setVolume(_masterVolume);
        _musicBus.setVolume(_musicVolume);
        _ambienceBus.setVolume(_ambienceVolume);
        _SFXBus.setVolume(_SFXVolume);
    }

    [Header("Volume")]
    [Range(0, 1)]
    public float _masterVolume = 1;
    [Range(0, 1)]
    public float _musicVolume = 1;
    [Range(0, 1)]
    public float _ambienceVolume = 1;
    [Range(0, 1)]
    public float _SFXVolume = 1;

    private Bus _masterBus;
    private Bus _musicBus;
    private Bus _ambienceBus;
    private Bus _SFXBus;


    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
