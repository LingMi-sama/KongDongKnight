using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField]private bool isLeak;
    [SerializeField]private bool isDead;

    private GameManager gameManager;
    private Animator animator;
    private CharacterEffect characterEffect;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        characterEffect = FindObjectOfType<CharacterEffect>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsDead();
        CheckLeakHealth();
    }
    private void CheckLeakHealth()
    {
        if (health == 1 && !isLeak)
        {
            isLeak = true;
            characterEffect.DoEffect(CharacterEffect.EffectType.LowHealth, true);
        }
        else
        {
            isLeak = false;
            characterEffect.DoEffect(CharacterEffect.EffectType.LowHealth, false);
        }
    
    }
    private void CheckIsDead()
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    private void Die()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("HeroDetector"), LayerMask.NameToLayer("EnemyDetector"), true);
        isDead = true;
        animator.SetTrigger("Dead");

        gameManager.SetEnableInput(false);
    
    }
    public int GetCurrentHealth()
    {
        return health;
    }
    public void LoseHealth(int health)
    {
        this.health -= health;
    }
    public bool GetDeadStatement()
    {
        CheckIsDead();
        return isDead;
    }


}
