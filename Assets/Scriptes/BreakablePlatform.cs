using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float breakForce = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private ParticleSystem particle;

    private Rigidbody2D rb2d;
    private bool isBroken = false;
    private float timeSinceTouched = 0f;
    private Vector3 initialPosition;
    private Sprite initialSprite;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialSprite = spriteRenderer.sprite;
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
            particle.transform.position = transform.position;
            particle.Play();
            gameObject.SetActive(false);
        }
    }

    private void Fall()
    {
        // D�sactive le Rigidbody2D pour faire tomber la plateforme
        rb2d.bodyType = RigidbodyType2D.Dynamic;

        // Applique une force pour que la plateforme tombe plus vite
        rb2d.AddForce(Vector2.down * breakForce, ForceMode2D.Impulse);

        // Appelle la m�thode "Respawn" apr�s 5 secondes
        Invoke("Respawn", 5f);
    }

    private void Respawn()
    {
        // R�active le Rigidbody2D pour faire revenir la plateforme
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        gameObject.SetActive(true);

        // R�initialise le sprite de la plateforme
        spriteRenderer.sprite = initialSprite;

        // Attend une seconde pour que la plateforme ait fini de revenir avant de pouvoir la toucher � nouveau
        Invoke("ResetTimeSinceTouched", 1f);
    }

    private void ResetTimeSinceTouched()
    {
        timeSinceTouched = 0f;
        isBroken = false;
    }
}