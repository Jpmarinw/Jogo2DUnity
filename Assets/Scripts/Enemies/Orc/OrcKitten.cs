using UnityEngine;

public class OrcKitten : MonoBehaviour
{
    public Transform orc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orc.GetComponent<OrcContoller>().enabled = true;
            orc.GetComponent<Animator>().SetBool("isRun", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orc.GetComponent<OrcContoller>().enabled = false;
            orc.GetComponent<Animator>().SetBool("isRun", false);
        }
    }
}
