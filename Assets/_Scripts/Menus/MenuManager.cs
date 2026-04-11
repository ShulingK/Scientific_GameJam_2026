using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] SceneLoader _sceneLoader;

    [SerializeField] Level _level;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlayMusic(FMODEvents.Instance._mainMenu);
    }

    public void ButtonSound()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance._clickButton);
    }

    public void Play()
    {
        _level.Reset();
        _level.Next();
        _sceneLoader.LoadScene(_level.level);
    }

    #region Settings

    [Header("Settings")]
    [SerializeField] GameObject _settingsPanel;

    public void OpenSettings(bool isOpen)
    {
        if (isOpen) 
            StartCoroutine(COpenSettings());
        else 
            StartCoroutine(CCloseSettings());
    }

    IEnumerator COpenSettings()
    {
        yield return null;

        _settingsPanel.SetActive(true);
    }

    IEnumerator CCloseSettings()
    {
        yield return null;

        _settingsPanel.SetActive(false);
    }
    #endregion

    #region Credits

    [Header("Credits")]
    [SerializeField] GameObject _creditsPanel;
    public void OpenCredits(bool isOpen)
    {
        if (isOpen)
            StartCoroutine(COpenCredits());
        else
            StartCoroutine(CCloseCredits());
    }

    IEnumerator COpenCredits()
    {
        yield return null;

        _creditsPanel.SetActive(true);
    }

    IEnumerator CCloseCredits()
    {
        yield return null;

        _creditsPanel.SetActive(false);
    }

    #endregion
}
