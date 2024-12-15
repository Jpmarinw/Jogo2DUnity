using UnityEngine;

public class OrcAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movimentacao player = collision.GetComponent<Movimentacao>();
            if (player != null)
            {
                player.TakeDamage(2);
            }
        }
    }
}
