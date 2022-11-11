using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public int[] minVolume;
    public Text[] volText;
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        float value;
        value = minVolume[0] / 10 * (10 - volume);//min=-60, -60 ~ 0
        audioMixer.SetFloat("MasterVolume", value);
        volText[0].text = volume.ToString();

    }
    public void SetSoundVolume(float volume)
    {
        float value;
        value = minVolume[1] / 10 * (10 - volume);
        audioMixer.SetFloat("SoundVolume", value);
        volText[1].text = volume.ToString();
    }
    public void SetMusicVolume(float volume)
    {
        float value;
        value = minVolume[2] / 10 * (10 - volume);
        audioMixer.SetFloat("MusicVolume", value);
        volText[2].text = volume.ToString();
    }
}
