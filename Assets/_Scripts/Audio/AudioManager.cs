using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using FMOD;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private IEnumerator Start()
    {
        // Charge les banks explicitement
        RuntimeManager.LoadBank("Master");
        RuntimeManager.LoadBank("Master.strings");

        // Attendre que FMOD soit prêt
        yield return new WaitForSeconds(0.5f);

        InitBuses();
    }
    private void InitBuses()
    {
        _masterBus = RuntimeManager.GetBus("bus:/");
        _musicBus = RuntimeManager.GetBus("bus:/Musique");
        _SFXBus = RuntimeManager.GetBus("bus:/SFX");
        _ambianceBus = RuntimeManager.GetBus("bus:/Ambiance");
        _voiceBus = RuntimeManager.GetBus("bus:/Dialogue");
    }

    private void Update()
    {
        if (!_masterBus.isValid()) return;

        _masterBus.setVolume(_masterVolume);
        _musicBus.setVolume(_musicVolume);
        _voiceBus.setVolume(_voiceVolume);
        _ambianceBus.setVolume(_ambienceVolume);
        _SFXBus.setVolume(_SFXVolume);
    }

    [Header("Volume")]
    [Range(0, 1)]
    public float _masterVolume = 1;
    [Range(0, 1)]
    public float _musicVolume = 1;
    [Range(0, 1)]
    public float _voiceVolume = 1;
    [Range(0, 1)]
    public float _ambienceVolume = 1;
    [Range(0, 1)]
    public float _SFXVolume = 1;

    private Bus _masterBus;
    private Bus _musicBus;
    private Bus _voiceBus;
    private Bus _ambianceBus;
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
