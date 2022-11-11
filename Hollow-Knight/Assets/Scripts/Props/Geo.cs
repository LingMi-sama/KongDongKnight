using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo : MonoBehaviour
{
    [SerializeField] private AudioClip[] geoHitGround;

    AudioSource audioSource;

    public bool isGround;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGround && collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isGround = true;
            int index = Random.Range(0, geoHitGround.Length);
            audioSource.PlayOneShot(geoHitGround[index]);
        
        }
    }


}
