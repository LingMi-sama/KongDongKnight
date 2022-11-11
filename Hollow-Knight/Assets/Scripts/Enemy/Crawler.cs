using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{

    bool forceMovement=true;
    private Rigidbody2D rigi;

    public Collider2D facingDetector;
    public ContactFilter2D contact;


    public GameObject groundCheck;

    public float hurtForce;
    public float deadForce;

    public int circleRadius;
    public LayerMask ground;

    bool isGrounded;

    public float movementSpeed;

    private Animator ani;
  

    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsDead();
        Direction();
        Movement();
        FacingDetect();
    }
    
    private void Movement()
    { 
    if(!isDead&&forceMovement)
        {
            if (isFacingRight)
            {
                rigi.velocity = Vector2.right * movementSpeed;

            }
            else
                rigi.velocity = Vector2.left * movementSpeed;
        }
    }
    
    private void FacingDetect()
    {
        if (isDead)
            return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, ground);

        if (!isGrounded)
        {
            Flip();
        }
        else {

            int count = Physics2D.OverlapCollider(facingDetector, contact,new List<Collider2D>() );

            
            if (count > 0)
            {
                Flip();
            }
        }                

    }

    private void Flip()
    {
        Vector3 vector = transform.localScale;
        vector.x *= -1;
        transform.localScale = vector;
    
    }

    public override void Hurt(int damage,Transform attackPosition)
    {
        base.Hurt(damage);
       
        Vector2 vector = transform.position - attackPosition.position;
        
        StartCoroutine(DelayHurt(vector));
       

    }
    IEnumerator DelayHurt(Vector2 vector)
    {
        rigi.velocity = Vector2.zero;
        forceMovement = false;
        if (vector.x > 0)
        {
            
            rigi.AddForce(new Vector2(hurtForce, 0),ForceMode2D.Impulse); }
        else
            rigi.AddForce(new Vector2(-hurtForce, 0),ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.3f);
        forceMovement = true;

    }
    protected override void Dead()
    {
        base.Dead();
        StartCoroutine(DelayDead());
  
    }
    IEnumerator DelayDead()
    {
   
        Vector3 diff = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        rigi.velocity = Vector2.zero;
        if (diff.x < 0)
        {
            rigi.AddForce(Vector2.right * deadForce,ForceMode2D.Impulse);
        }
        else
            rigi.AddForce(Vector2.left * deadForce,ForceMode2D.Impulse);

        if (ani != null)
        {
           
            ani.SetBool("Dead", true);
        }
        yield return new WaitForSeconds(0.2f);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
    
    }



}
