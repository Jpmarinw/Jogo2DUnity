using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengh;
    private float startPos;
    private Transform cam;
    public float parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        lengh = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float rePos = cam.transform.position.x + (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (rePos > startPos + lengh)
        {
            startPos += lengh;
        }
        else if (rePos < startPos - lengh)
        {
            startPos -= lengh;
        }
    }
}
