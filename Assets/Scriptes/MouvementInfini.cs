using UnityEngine;

public class MouvementInfini : MonoBehaviour
{
    public float vitesse = 1f;
    public float frequence = 1f;
    public float lerpSpeed = 10f;
    public float amplitudeMin = 1f;
    public float amplitudeMax = 2.5f;

    private Vector3 positionInitiale;
    private Vector3 targetPosition;
    private float currentAmplitude;
    private float amplitudeChangeTimer = 0f;
    private float amplitudeChangeDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private float xOffset;
    private float yOffset;
    private float previousHorizontalPosition;

    private void Start()
    {
        positionInitiale = transform.position;
        currentAmplitude = Random.Range(amplitudeMin, amplitudeMax);
        spriteRenderer = GetComponent<SpriteRenderer>();
        xOffset = Random.Range(0f, 100f);
        yOffset = Random.Range(0f, 100f);
        previousHorizontalPosition = transform.position.x;
    }

    private void Update()
    {
        amplitudeChangeTimer += Time.deltaTime;

        if (amplitudeChangeTimer >= amplitudeChangeDuration)
        {
            amplitudeChangeTimer = 0f;
            currentAmplitude = Random.Range(amplitudeMin, amplitudeMax);
        }

        float deplacementHorizontal = Mathf.PerlinNoise(xOffset + Time.time * frequence, 0f) * currentAmplitude;
        float deplacementVertical = Mathf.PerlinNoise(0f, yOffset + Time.time * frequence) * (currentAmplitude / 2f);

        Vector3 deplacement = new Vector3(deplacementHorizontal, deplacementVertical, 0f);
        targetPosition = positionInitiale + deplacement;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime * vitesse);

        // Inversion du sprite renderer en fonction de la direction horizontale
        if (transform.position.x < previousHorizontalPosition)
        {
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x > previousHorizontalPosition)
        {
            spriteRenderer.flipX = false;
        }

        previousHorizontalPosition = transform.position.x;
    }
}