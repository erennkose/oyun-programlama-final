using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuUIManagerScript : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    Slider slider;

    float masterVolume;

    void Start()
    {
        audioMixer.GetFloat("MasterVolume", out masterVolume);
        slider.value = MathF.Pow(10f, Mathf.Clamp(masterVolume, -80f, 20f) / 20f);
    }

    public void PlayButton()
    {
        SceneLoader.LoadScene(1);
    }

    public void GithubButton()
    {
        Application.OpenURL("https://github.com/erennkose/oyun-programlama-final");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    // Master sesi ayarlama amaçlı fonksiyon
    public void MasterVolumeSlider(float value)
    {
        float volume =  Mathf.Clamp(value, 0.0001f, 10f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }
}
