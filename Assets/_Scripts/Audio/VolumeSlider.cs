using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER, 
        MUSIC, 
        VOICE, 
        AMBIANCE, 
        SFX
    }

    [SerializeField] VolumeType _volumeType;

    private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch(_volumeType)
        {
            case VolumeType.MASTER:
                _volumeSlider.value = AudioManager.Instance._masterVolume;
                break;
            case VolumeType.MUSIC:
                _volumeSlider.value = AudioManager.Instance._musicVolume;
                break;
            case VolumeType.VOICE:
                _volumeSlider.value = AudioManager.Instance._voiceVolume;
                break;
            case VolumeType.SFX:
                _volumeSlider.value = AudioManager.Instance._SFXVolume;
                break;
            case VolumeType.AMBIANCE:
                _volumeSlider.value = AudioManager.Instance._ambienceVolume;
                break;
            default:
                Debug.LogWarning("VolumeType non valide");
                break;
        }
    }


    public void OnSliderValueChange()
    {
        switch(_volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.Instance._masterVolume = _volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.Instance._musicVolume = _volumeSlider.value;
                break;
            case VolumeType.VOICE:
                AudioManager.Instance._voiceVolume = _volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.Instance._SFXVolume = _volumeSlider.value;
                break;
            case VolumeType.AMBIANCE:
                AudioManager.Instance._ambienceVolume = _volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VolumeType non valide");
                break;
        }

        AudioManager.Instance.SaveVolumes();
    }
}
