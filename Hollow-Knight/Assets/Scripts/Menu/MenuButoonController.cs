using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButoonController : MonoBehaviour
{

    public Animator logoTitle;
    public Animator mainMenuScreen;
    public Animator audioMenuScreen;
    private AudioSource audioSource;
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(DelayDisplayOpening());
    
    }

    IEnumerator DelayDisplayOpening()
    {
        logoTitle.Play("FadeOut");
        mainMenuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        StartCoroutine(DelayDisplayAudioMenu());
    }
    IEnumerator DelayDisplayAudioMenu()
    {
        logoTitle.Play("TitleFadeOut");
        mainMenuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        audioMenuScreen.Play("FadeIn");
    
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitAudioMenu()
    {
        StartCoroutine(DelayShutAudioMenu());
    }
    IEnumerator DelayShutAudioMenu()
    {
        audioMenuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        logoTitle.Play("TitleFadeIn");
        mainMenuScreen.Play("FadeIn");
    }
    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
   
}
