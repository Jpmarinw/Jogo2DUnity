using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movimentacao player = collision.GetComponent<Movimentacao>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}