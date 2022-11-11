using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator[] healthItem;
    public Animator geo;

    private CharacterData characterData;
    void Start()
    {
        characterData = FindObjectOfType<CharacterData>();
    }

    public void Hurt()
    {
        if (characterData.GetDeadStatement())
            return;
        characterData.LoseHealth(1);
        int health = characterData.GetCurrentHealth();

        healthItem[health].SetTrigger("Hurt");
    
    }
    public IEnumerator ShowHealthItems()
    {
        for (int i = 0; i < healthItem.Length; i++)
        {
            healthItem[i].SetTrigger("Respawn");
            yield return new WaitForSeconds(0.2f);
        
        }
        yield return new WaitForSeconds(0.2f);
        geo.Play("Enter");
    }
    public void HideHealthItems()
    {
        geo.Play("Exit");
        for (int i = 0; i < healthItem.Length; i++)
        {
            healthItem[i].SetTrigger("Hide");
           
        }
    }
}
