using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount = 1; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Orc"))
        {
            OrcContoller orc = collision.GetComponent<OrcContoller>();
            if (orc != null)
            {
                orc.TakeDamage(damageAmount);
            }
        }

        if (collision.CompareTag("Ghost"))
        {
            Ghost ghost = collision.GetComponent<Ghost>();
            if (ghost != null)
            {
                ghost.TakeDamage(damageAmount);
            }
        }
    }
}
