using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoCollect : MonoBehaviour
{
    [SerializeField]Animator collectAni;
    [SerializeField]AudioClip[] geoCollect;
    [SerializeField]int geoCount = 0;
    [SerializeField]Text geoText;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        geoText.text = geoCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Geo"))
        {
            collectAni.SetTrigger("Collect");
            int index = Random.Range(0, geoCollect.Length);
            audioSource.PlayOneShot(geoCollect[index]);

            geoCount++;
            geoText.text = geoCount.ToString();
            Destroy(collision.gameObject);
        }
    }
}
