using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;
using FMOD;

public class AudioManager : MonoBehaviour
{
    private const string MASTER_KEY = "volume_master";
    private const string MUSIC_KEY = "volume_music";
    private const string VOICE_KEY = "volume_voice";
    private const string AMBIENCE_KEY = "volume_ambience";
    private const string SFX_KEY = "volume_sfx";

    private const float AMBIENCE_MULTIPLIER = 0.2f;
    private const float MUSIC_MULTIPLIER = 0.4f;
    private const float SFX_MULTIPLIER = 0.5f;
    private const float VOICE_MULTIPLIER = 1f;

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

        LoadVolumes(); 

        // Attendre que FMOD soit prêt
        yield return new WaitForSeconds(0.5f);

        InitBuses();
    }
    private void LoadVolumes()
    {
        _masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        _musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.4f);
        _voiceVolume = PlayerPrefs.GetFloat(VOICE_KEY, 0.8f);
        _ambienceVolume = PlayerPrefs.GetFloat(AMBIENCE_KEY, 0.4f);
        _SFXVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.8f);
    }
    public void SaveVolumes()
    {
        PlayerPrefs.SetFloat(MASTER_KEY, _masterVolume);
        PlayerPrefs.SetFloat(MUSIC_KEY, _musicVolume);
        PlayerPrefs.SetFloat(VOICE_KEY, _voiceVolume);
        PlayerPrefs.SetFloat(AMBIENCE_KEY, _ambienceVolume);
        PlayerPrefs.SetFloat(SFX_KEY, _SFXVolume);

        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        SaveVolumes();
    }

    private void InitBuses()
    {
        _masterBus = RuntimeManager.GetBus("bus:/");
        _musicBus = RuntimeManager.GetBus("bus:/Musique");
        _SFXBus = RuntimeManager.GetBus("bus:/SFX");
        _ambianceBus = RuntimeManager.GetBus("bus:/Ambiance");
        _voiceBus = RuntimeManager.GetBus("bus:/Voice");
    }

    private void Update()
    {
        if (!_masterBus.isValid()) return;

        _masterBus.setVolume(_masterVolume);
        _musicBus.setVolume(_musicVolume * MUSIC_MULTIPLIER);
        _ambianceBus.setVolume(_ambienceVolume * AMBIENCE_MULTIPLIER);
        _voiceBus.setVolume(_voiceVolume * VOICE_MULTIPLIER);
        _SFXBus.setVolume(_SFXVolume * SFX_MULTIPLIER);
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

    private EventInstance _musicInstance;

    public void PlayMusic(EventReference musicEvent)
    {
        // Stop ancienne musique si besoin
        if (_musicInstance.isValid())
        {
            _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _musicInstance.release();
        }

        _musicInstance = RuntimeManager.CreateInstance(musicEvent);
        _musicInstance.start();
    }
    public void StopMusic()
    {
        if (!_musicInstance.isValid()) return;

        _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicInstance.release();
    }

    private EventInstance _ambianceInstance;

    public void PlayAmbiance(EventReference musicEvent)
    {
        // Stop ancienne musique si besoin
        if (_ambianceInstance.isValid())
        {
            _ambianceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _ambianceInstance.release();
        }

        _ambianceInstance = RuntimeManager.CreateInstance(musicEvent);
        _ambianceInstance.start();
    }
    public void StopAmbiance()
    {
        if (!_ambianceInstance.isValid()) return;

        _ambianceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _ambianceInstance.release();
    }

    private EventInstance _dialogueSnapshot;

    public void StartDialogueSnapshot(EventReference dialogue)
    {
        if (_dialogueSnapshot.isValid()) return;

        _dialogueSnapshot = RuntimeManager.CreateInstance(dialogue);
        _dialogueSnapshot.start();
    }

    public void StopDialogueSnapshot()
    {
        if (!_dialogueSnapshot.isValid()) return;

        _dialogueSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _dialogueSnapshot.release();
        _dialogueSnapshot.clearHandle();
    }

}
