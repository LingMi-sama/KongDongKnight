using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    bool waiting = false;

    public void Stop(float duration, float timeScale)
    {
        if (waiting)
            return;
        Time.timeScale = timeScale;
        StartCoroutine(Wait(duration));

    }
    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    
    }
}
