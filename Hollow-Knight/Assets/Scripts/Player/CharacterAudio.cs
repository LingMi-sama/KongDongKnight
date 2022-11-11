using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource landingAudio;
    [SerializeField] AudioSource fallingAudio;
    [SerializeField] AudioSource takeDamageAudio;

    public enum AudioType
    {
        Jump, Landing, Falling, TakeDamage
    }

    public void Play(AudioType audioType, bool playState)
    {
        AudioSource audioSource = null;
        switch (audioType)
        {
            case AudioType.Jump:
                audioSource = jumpAudio;
                break;
            case AudioType.Landing:
                audioSource = landingAudio;
                break;
            case AudioType.Falling:
                audioSource = fallingAudio;
                break;
            case AudioType.TakeDamage:
                audioSource = takeDamageAudio;
                break;

        }
        if (audioSource != null)
        {
            if (playState)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}
