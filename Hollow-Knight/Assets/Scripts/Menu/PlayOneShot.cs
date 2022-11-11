using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    public void ButtonPlayOneShot(AudioClip clip)
    {
        FindObjectOfType<MenuAudio>().PlayOneShot(clip);
    }
}
