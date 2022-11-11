using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("移动参数")]
    [SerializeField] float hurtForce = 1f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] public float jumpTimer = 0.5f;
    [SerializeField] Vector3 flippedScale = new Vector3(-1, 1, 1);
    [SerializeField] int moveChangeAni;
    float moveX;
    float moveY;
    [Header("引用组件")]
    private CharacterEffect characterEffect;
    private CharacterAudio characterAudio;
    private Rigidbody2D rigi;
    private Animator animator;
    private CinemaShaking cinemaShaking;
    private Attack attack;
    private GameManager gameManager;
    private AudioSource audio;
    [Header("状态判断")]
    bool isFacingRight;
    bool isOnGround;
    bool canMove;
    [SerializeField] bool firstLanding;
    [Header("攻击参数")]
    [SerializeField] float slashIntervalTime = 0.2f;
    [SerializeField] float maxComboTime = 0.4f;
    [SerializeField] float recoilForce;
    [SerializeField] int slashCount;
    [SerializeField] int slashDamage = 1;
    [SerializeField] float downRecoilForce;
    float lastSlashTime;




    void Start()
    {
        characterEffect = FindObjectOfType<CharacterEffect>();
        characterAudio = FindObjectOfType<CharacterAudio>();
        attack = FindObjectOfType<Attack>();
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        cinemaShaking = FindObjectOfType<CinemaShaking>();
        audio = GetComponent<AudioSource>();

        canMove = true;


    }

    // Update is called once per frame
    void Update()
    {

        ResetComboTime();
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    TakeDamage();
        //}
        Movement();
        Direction();
        Jump();
        PlayerAttack();
        animator.SetBool("FirstLanding", firstLanding);
    }

    private void Movement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (canMove && gameManager.IsEnableInput())
        {
            rigi.velocity = new Vector2(moveX * moveSpeed, rigi.velocity.y);
        }


        if (moveX > 0)
        {
            isFacingRight = true;
            moveChangeAni = 1;
        }
        else if (moveX < 0)
        {
            isFacingRight = false;
            moveChangeAni = -1;
        }
        else moveChangeAni = 0;

        animator.SetInteger("movement", moveChangeAni);

        animator.SetFloat("VelocityY", rigi.velocity.y);

    }

    private void Direction()
    {
        if (gameManager.IsEnableInput())
        {
            if (moveX > 0)
            {

                transform.localScale = flippedScale;
            }
            else if (moveX < 0)
            {
                transform.localScale = Vector3.one;
            }
        }

    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (!gameManager.IsEnableInput())
                return;
            jumpTimer -= Time.deltaTime;

            if (jumpTimer > 0)
            {

                rigi.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);

                animator.SetTrigger("jump");

                characterAudio.Play(CharacterAudio.AudioType.Jump, true);


            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grouding(collision, false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Grouding(collision, false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Grouding(collision, true);
    }
    private void Grouding(Collision2D col, bool exitState)
    {
        if (exitState)//离开为真
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                isOnGround = false;
        }
        else
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.up)//从上往下
            {
                characterEffect.DoEffect(CharacterEffect.EffectType.FallTrail, true);

                isOnGround = true;
                
                JumpCancle();
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.down)
            {
                JumpCancle();
            }

        }
        animator.SetBool("isOnGround", isOnGround);

    }
    private void JumpCancle()
    {
        animator.ResetTrigger("jump");
        jumpTimer = 0.5f;

    }
    public void TakeDamage()
    {
        cinemaShaking.CinemaShake();

        StartCoroutine(FindObjectOfType<Invincibility>().SetInvincibility());
        FindObjectOfType<Health>().Hurt();
        if (isFacingRight)
        {
            rigi.velocity = new Vector2(1, 1) * hurtForce;
        }
        else
            rigi.velocity = new Vector2(-1, 1) * hurtForce;

        animator.Play("TakeDamage");

       characterAudio.Play(CharacterAudio.AudioType.TakeDamage, true);

    }
    public void PlayHitParticals()
    {
        characterEffect.DoEffect(CharacterEffect.EffectType.HitL, true);
        characterEffect.DoEffect(CharacterEffect.EffectType.HitR, true);
    
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))//且人物活着
        {
            if (!gameManager.IsEnableInput())
                return;
            if (Time.time >= lastSlashTime + slashIntervalTime)
            {
                lastSlashTime = Time.time;
                if (moveY > 0)
                {
                    SlashAndDetect(Attack.AttackType.Upslash);
                    animator.Play("UpSlash");

                }
                else if (!isOnGround && moveY < 0)
                {
                    SlashAndDetect(Attack.AttackType.DownSlash);
                    animator.Play("DownSlash");
                }
                else
                {
                    slashCount++;
                    switch (slashCount)
                    {
                        case 1:
                            SlashAndDetect(Attack.AttackType.Slash);
                            animator.Play("Slash");
                            break;
                        case 2:
                            SlashAndDetect(Attack.AttackType.AltSlash);
                            animator.Play("AltSlash");
                            slashCount = 0;
                            break;
                    }
                }


            }


        }


    }
    private void ResetComboTime()
    {
        if (Time.time >= lastSlashTime + maxComboTime && slashCount != 0)
        {
            slashCount = 0;
        }

    }
    private void SlashAndDetect(Attack.AttackType attackType)
    {

        List<Collider2D> colliders = new List<Collider2D>();
        attack.Play(attackType, ref colliders);

        bool hasEnemy = false;
        bool hasDamagePlayer = false;


        //检测是否是敌人
        foreach (Collider2D col in colliders)
        {

            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyDetector"))
            {
                hasEnemy = true;

                break;

            }

        }
        foreach (Collider2D col in colliders)
        {

            if (col.gameObject.layer == LayerMask.NameToLayer("DamagePlayer"))
            {
                hasDamagePlayer = true;
                break;

            }
        }
        if (hasEnemy)
        {
            //Recoil
            if (attackType == Attack.AttackType.DownSlash)
            {
                AddDownRecoilForce();

            }
            else
            {
                StartCoroutine(AddRecoilForce());

            }
        }
        if (hasDamagePlayer)
        {
            if (attackType == Attack.AttackType.DownSlash)
            {
                AddDownRecoilForce();
            }
        }

        foreach (Collider2D col in colliders)
        {

            Breakable breakable = col.GetComponent<Breakable>();
            if (breakable != null)
            {
                breakable.Hurt(slashDamage, transform);
            }

        }

    }
    private void AddDownRecoilForce()
    {
        rigi.velocity.Set(rigi.velocity.x, 0);
        rigi.AddForce(Vector2.up * downRecoilForce, ForceMode2D.Impulse);
    }
    IEnumerator AddRecoilForce()
    {
        canMove = false;
        if (isFacingRight)
        {
            rigi.AddForce(Vector2.left * recoilForce, ForceMode2D.Impulse);

        }
        yield return new WaitForSeconds(0.2f);
        canMove = true;

    }
    public void FirstLand()
    {
        StopInput();
        characterEffect.DoEffect(CharacterEffect.EffectType.BurstRocks, true);
    
    }
    public void StopInput()
    {
        gameManager.SetEnableInput(false);
        StopHorizontalMovement();
    }
    public void ResumeInput()
    {
        gameManager.SetEnableInput(true);
        firstLanding = true;

        FindObjectOfType<SoulOrb>().DelayShowOrb(0.1f);

    }
    public void StopHorizontalMovement()
    {
        Vector2 velocity = rigi.velocity;
        velocity.x = 0;
        rigi.velocity = velocity;
        animator.SetInteger("movement", 0);
    }
    public void PlayMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

}
