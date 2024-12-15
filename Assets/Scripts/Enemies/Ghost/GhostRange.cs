using UnityEngine;

public class GhostRange : MonoBehaviour
{
    private Ghost ghost; // Referência ao script do Ghost

    private void Start()
    {
        // Conectar o script do Ghost (Script Pai do Collider)
        ghost = GetComponentInParent<Ghost>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Se o jogador estiver no Range, iniciar o ataque
        if (collision.CompareTag("Player"))
        {
            ghost.StartAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Se o jogador sair do Range, parar o ataque e voltar à patrulha
        if (collision.CompareTag("Player"))
        {
            ghost.StopAttack();
        }
    }
}
