using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private CapsuleCollider2D colliderGhost;
    private Animator anim;
    private bool goRight;
    private bool isAttacking;

    public int life;
    public float speed;
    public Transform a;
    public Transform b;
    public GameObject range;

    public AudioSource audioSource;   
    public AudioClip hitSound;       
    public AudioClip deathSound;     

    void Start()
    {
        colliderGhost = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        isAttacking = false; // Inicializamos como falso

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Verificações adicionais para evitar NullReferenceException
        if (colliderGhost == null)
        {
            Debug.LogError("CapsuleCollider2D não encontrado!");
        }
        if (anim == null)
        {
            Debug.LogError("Animator não encontrado!");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado!");
        }
    }

    void Update()
    {
        if (life <= 0)
        {
            Die();
            return;
        }


        if (isAttacking) return;

        if (goRight)
        {
            // Caso atinja o ponto B, inverte a direção
            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false;
            }

            // Definir a rotação e mover para B
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        }
        else
        {
            // Caso atinja o ponto A, inverte a direção
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true;
            }

            // Definir a rotação e mover para A
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        anim.Play("Attack", -1);
    }

    public void StopAttack()
    {
        isAttacking = false;

        anim.Play("Walk", -1);
    }

    public void TakeDamage(int damageAmount)
    {
        life -= damageAmount;

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.enabled = false;
        colliderGhost.enabled = false;
        range.SetActive(false);
        anim.Play("Die");

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
