using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrcContoller : MonoBehaviour
{
    private CapsuleCollider2D colliderOrc;
    private Animator anim;
    private float sideSign;
    private string side;
    public GameObject range;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public int life;
    public float speed;
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliderOrc = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (colliderOrc == null)
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
            StartCoroutine(LoadSceneWithDelay());
            return;
        }

        if (anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        sideSign = Mathf.Sign(transform.position.x - player.position.x);

        if (Mathf.Abs(sideSign) == 1.0f)
        {
            side = sideSign == 1.0f ? "right" : "left";
        }

        switch (side)
        {
            case "right":
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                break;

            case "left":
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;
        }

        if (Vector2.Distance(transform.position, player.position) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
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
        colliderOrc.enabled = false;
        range.SetActive(false);
        anim.Play("Die");

        // Tocar o som de morte
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        StartCoroutine(LoadSceneWithDelay());
    }
}
