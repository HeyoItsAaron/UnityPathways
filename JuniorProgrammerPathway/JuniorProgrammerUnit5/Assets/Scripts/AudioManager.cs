using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private List<AudioClip> squishClips;
    [SerializeField] private AudioClip boomClip;

    [SerializeField] private GameObject audioSettings;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private AudioSource musicAudioSource;
    private AudioSource sfxAudioSource;


    private void Awake()
    {
        if (AudioManager.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            AudioManager.instance = this;
        }
        sfxAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        sfxVolumeSlider.value = sfxAudioSource.volume;
        musicVolumeSlider.value = musicAudioSource.volume;

        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);

        audioSettings.SetActive(false);
    }

    public void PlaySquishClip()
    {
        sfxAudioSource.PlayOneShot(squishClips[Random.Range(0, squishClips.Count)]);
    }
    public void PlayBoomClip()
    {
        sfxAudioSource.PlayOneShot(boomClip);
    }

    private void OnSFXVolumeSliderChanged(float volume)
    {
        sfxAudioSource.volume = volume;
    }
    private void OnMusicVolumeSliderChanged(float volume)
    {
        musicAudioSource.volume = volume;
    }
    public void ToggleAudioSettings()
    {
        audioSettings.SetActive(!audioSettings.activeInHierarchy);
    }
}
