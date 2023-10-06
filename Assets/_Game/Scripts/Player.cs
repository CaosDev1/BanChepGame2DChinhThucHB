using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    private bool isAttack = false;
    private bool attackKey;
    private bool throwKey;
    
    [Header("Move Info")]
    [SerializeField] private float speed;
    private float horizontalInput;
    [SerializeField] private float jumpForce;
    private bool jumpKey;
    private bool isJumping;

    [Header("Collision Info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;

    [Header("Attack Info")]
    [SerializeField] private Kunai kunaiPrefabs;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

    private int coin = 0;
    private Vector3 savePoint;

    
    public static Player Instance;


    private void Awake()
    {
        Instance = this;
        coin = PlayerPrefs.GetInt("coin", 0);

    }
    private void Update()
    {
        if (IsDead) return;

        CollisonCheck();
        PlayerInput();

        if (isAttack)
        {
            return;
        }
        else
        {
            JumpButton();
            Move();
        }

        AttackButton();
        ThrowButton();
    }

    public override  void OnInit()
    {
        base.OnInit();
        
        isAttack= false;

        transform.position= savePoint;
        
        ChangeAnim("idle");
        DeActiveAttack();
        SavePoint();
        UIManager.Instance.SetCoin(coin);
        TurnOffGravity();
    }

    public void TurnOnGravity()
    {
        rb.gravityScale = 4f;
    }

    public void TurnOffGravity()
    {
        rb.gravityScale = 0f;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpKey = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W));
        attackKey = Input.GetKeyDown(KeyCode.C);
        throwKey = Input.GetKeyDown(KeyCode.V);
    }

    public void AttackButton()
    {
        if (attackKey && isGrounded)
        {
            Attack();
        }
    }

    public void ThrowButton()
    {
        if (throwKey && isGrounded)
        {
            Throw();
        }
    }

    private void Move()
    {
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            //anim run
            if (!isJumping && isGrounded)
                ChangeAnim("run");

            //run acction
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            Flip();
        }
        else if (isGrounded && rb.velocity.y <= 0 && !isAttack)
        {
            //anim idle
            ChangeAnim("idle");

            //idle acction
            rb.velocity = Vector2.zero;
        }
    }

    private void JumpButton()
    {
        if (jumpKey && isGrounded)
        {
            Jump();
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            //anim fall
            ChangeAnim("fall");
        }

        if (isGrounded)
        {
            isJumping = false;
        }

    }

    public void Jump()
    {
        //anim jump
        ChangeAnim("jump");

        //jump acction
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        isJumping = true;
    }

    public void Attack()
    {
        isAttack = true;
        ChangeAnim("attack");
        rb.velocity = Vector2.zero;
        Invoke(nameof(ResetAttack), 0.5f);

        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    private void ResetAttack()
    {
        isAttack = false;
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void Throw()
    {
        isAttack = true;
        ChangeAnim("throw");
        rb.velocity = Vector2.zero;
        Invoke(nameof(ResetThrow), 0.5f);

        Instantiate(kunaiPrefabs, transform.position, transform.rotation);
    }

    private void ResetThrow()
    {
        isAttack = false;
    }

    private void Flip()
    {
        transform.rotation = Quaternion.Euler(0, horizontalInput > 0.1f ? 0 : 180, 0);
    }

    private void CollisonCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Coin"))
        {
            coin++;
            //PlayerPrefs.SetInt("coin", coin);
            UIManager.Instance.SetCoin(coin);
            Destroy(collision.gameObject);
            Debug.Log("Coin: " + coin);
        }

        if (collision.tag == ("DeathZone"))
        {
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    public void SetMove(float horizontal)
    {
        this.horizontalInput = horizontal;
    }
}
