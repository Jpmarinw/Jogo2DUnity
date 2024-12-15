using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Movimentacao : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public TextMeshProUGUI textLife;
    private BoxCollider2D colliderPlayer;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    private bool facingRight = true;
    public int addJumps;
    public bool isGrounded;
    public int life;

    private const string GroundTag = "Ground";
    private const string IsWalkAnimParam = "isWalk";
    private const string IsRunAnimParam = "isRun";
    private const string IsJumpAnimParam = "isJump";
    private const string Attack1AnimParam = "isAttack1";
    private const string Attack2AnimParam = "isAttack2";
    private const string Attack3AnimParam = "isAttack3";

    private int attackStep = 0;
    private float lastAttackTime = 0f;
    public float comboResetTime = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliderPlayer = GetComponent<BoxCollider2D>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        Move();
        Attack();
        textLife.text = life.ToString();
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida do personagem
        life -= damageAmount;

        // Tocar o som de "dano"
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Verificar se a vida chegou a zero
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.enabled = false;
        colliderPlayer.enabled = false;
        rb.gravityScale = 0;

        anim.Play("Dead", -1);

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        StartCoroutine(LoadSceneWithDelay());
    }


    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        transform.position += new Vector3(moveX, 0, 0) * currentSpeed * Time.deltaTime;

        if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight))
        {
            Flip();
        }

        anim.SetBool(IsWalkAnimParam, moveX != 0 && !isRunning);
        anim.SetBool(IsRunAnimParam, moveX != 0 && isRunning);

        if (Input.GetButtonDown("Jump") && (isGrounded || addJumps > 0))
        {
            Jump();
            if (!isGrounded)
            {
                addJumps--;
            }
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime > comboResetTime)
            {
                attackStep = 0;
            }

            lastAttackTime = Time.time;

            if (attackStep == 0)
            {
                anim.SetTrigger(Attack1AnimParam);
                attackStep++;
            }
            else if (attackStep == 1)
            {
                anim.SetTrigger(Attack2AnimParam);
                attackStep++;
            }
            else if (attackStep == 2)
            {
                anim.SetTrigger(Attack3AnimParam);
                attackStep = 0;
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetBool(IsJumpAnimParam, true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GroundTag))
        {
            isGrounded = true;
            anim.SetBool(IsJumpAnimParam, false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GroundTag))
        {
            isGrounded = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
