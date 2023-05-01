using UnityEngine;

public class ParallaxNuage : MonoBehaviour
{
    public GameObject backgroundParent;
    public float parallaxFactor;
    private float backgroundSize;

    void Start()
    {
        backgroundSize = backgroundParent.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float cameraPosition = Camera.main.transform.position.x;
        float backgroundPosition = cameraPosition * parallaxFactor;
        float offset = backgroundPosition % backgroundSize;
        backgroundParent.transform.position = new Vector2(cameraPosition + offset, backgroundParent.transform.position.y);
    }
}
