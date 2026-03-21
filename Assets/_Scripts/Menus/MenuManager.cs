using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] SceneLoader _sceneLoader;

    [SerializeField] Level _level;

    private EventInstance musicEventInstance;

    private void Start()
    {
        InitializeMusic(FMODEvents.Instance._mainMenu);
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = AudioManager.Instance.CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void Play()
    {
        _sceneLoader.LoadScene(_level.level);
    }

    #region Settings
    public void OpenSettings(bool isOpen)
    {

    }

    IEnumerator COpenSettings()
    {
        yield return null;

    }

    IEnumerator CCloseSettings()
    {
        yield return null;

    }
    #endregion

    #region Credits
    public void OpenCredits(bool isOpen)
    {
        // 
    }

    IEnumerator COpenCredits()
    {
        yield return null;

    }

    IEnumerator CCloseCredits()
    {
        yield return null;
    }
    #endregion
}
