using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER, 
        MUSIC, 
        AMBIENCE, 
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
                break;
            case VolumeType.MUSIC:
                break;
            case VolumeType.AMBIENCE:
                break;
            case VolumeType.SFX:
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
            case VolumeType.AMBIENCE:
                AudioManager.Instance._ambienceVolume = _volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.Instance._SFXVolume = _volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VolumeType non valide");
                break;
        }
    }
}
