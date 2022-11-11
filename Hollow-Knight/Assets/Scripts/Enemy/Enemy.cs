using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Breakable
{
    public GameObject coin;
    int randomCount;

    [SerializeField]protected int minSpawnCoins = 2;
    [SerializeField] protected int maxSpawnCoins = 5;
    [SerializeField] protected float maxBumpXForce = 100;
    [SerializeField] protected float minBumpYForce = 300;
    [SerializeField] protected float maxBumpYForce = 500;

    protected bool isFacingRight;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Direction()
    {
        if (transform.localScale.x == 1)
        {
            isFacingRight = true;
        }
        else if (transform.localScale.x == -1)
        {
            isFacingRight = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectCollisionEnter2D(collision);
    }
    protected virtual void DetectCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HeroDetector"))
        {
            FindObjectOfType<PlayerController>().TakeDamage();
            FindObjectOfType<HitPause>().Stop(0.5f, 0.0f);
        }
        if (isDead && collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<BoxCollider2D>().enabled = false;
        
        }
    }
    protected override void Dead()
    {
        base.Dead();
        SpawnCoins();
        
    }
    public virtual void SpawnCoins()
    {
        randomCount = Random.Range(minSpawnCoins,maxSpawnCoins);//2,3,4
        for (int i = 0; i < randomCount; i++)
        {
            GameObject geo = Instantiate(coin, transform.position, Quaternion.identity, transform.parent);
            Vector2 force = new Vector2(Random.Range(-maxBumpXForce,maxBumpXForce), Random.Range(minBumpYForce,maxBumpYForce));
            geo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        
        }

    }


}
