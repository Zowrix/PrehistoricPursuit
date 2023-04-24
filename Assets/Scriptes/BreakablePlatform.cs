using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float breakForce = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private ParticleSystem particle;

    //[SerializeField] private AudioClip breakSound;

    private Rigidbody2D rb2d;
    private bool isBroken = false;
    private float timeSinceTouched = 0f;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isBroken)
        {
            return;
        }

        if (timeSinceTouched > 0)
        {
            timeSinceTouched -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.collider.CompareTag("Player") && rb2d.velocity.y <= 0f && timeSinceTouched <= 0f)
        {
            // Le joueur marche sur la plateforme et elle tombe
            timeSinceTouched = fallDelay;
            // Change le sprite et joue le son de destruction
            spriteRenderer.sprite = brokenSprite;
            Invoke("Fall", fallDelay);
        }

        if (collision.collider.CompareTag("Sol"))
        {
            Debug.Log("touche le sol");
            particle.transform.position = transform.position;
            particle.Play();
            Destroy(gameObject);
        }
    }

    private void Fall()
    {
        // Désactive le Rigidbody2D pour faire tomber la plateforme
        rb2d.bodyType = RigidbodyType2D.Dynamic;

        // Applique une force pour que la plateforme tombe plus vite
        rb2d.AddForce(Vector2.down * breakForce, ForceMode2D.Impulse);


        //AudioSource.PlayClipAtPoint(breakSound, transform.position);


    }
}