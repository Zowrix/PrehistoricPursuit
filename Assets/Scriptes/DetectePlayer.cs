using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectePlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public float detectionRange = 30f;
    public float fireInterval = 2f;
    private float fireTimer = 0f;
    public GameObject projectilePrefab; // Ajout de la r�f�rence au prefab du projectile
    public float projectileSpeed = 5f;
    public Vector2 offset;
    private Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // V�rifiez si le joueur est dans la plage de d�tection
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < detectionRange)
        {
            // V�rifiez si le d�lai de tir est �coul�
            if (fireTimer <= 0f)
            {
                Fire(); // Fonction pour tirer
                fireTimer = fireInterval; // R�initialiser le d�lai de tir
            }
            else
            {
                fireTimer -= Time.deltaTime; // D�cr�menter le d�lai de tir
            }
        }
        // Tourner le sprite de l'ennemi pour faire face au joueur
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Faire face � gauche
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1); // Faire face � droite
            }
        }
    }

    private void Fire()
    {
        // Cr�er un projectile � partir de l'ennemi avec le d�calage de position
        GameObject projectile = Instantiate(projectilePrefab, (Vector2)transform.position + GetProjectileOffset(), Quaternion.identity);

        // D�sactiver la gravit� du projectile
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.gravityScale = 0f;

        // Calculer la direction vers le joueur
        Vector2 direction = (player.transform.position - projectile.transform.position).normalized;

        // Appliquer une force au projectile pour le d�placer dans la direction du joueur
        projectileRigidbody.velocity = direction * projectileSpeed;

        // D�truire le projectile apr�s un laps de temps sp�cifi�
        StartCoroutine(DestroyProjectileAfterDelay(projectile, 3f));

        // D�clencher l'animation avant le tir
        if (enemyAnimator != null)
        {
            // D�clencher l'animation "Fire"
            enemyAnimator.SetTrigger("Fire");

            // Obtenir la dur�e totale de l'animation "Fire"
            float animationDuration = enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            // D�marrer une coroutine pour r�initialiser le d�clencheur "Fire" apr�s la dur�e de l'animation
            StartCoroutine(ResetFireTrigger(animationDuration));
        }
    }

    private Vector2 GetProjectileOffset()
    {
        // Obtenir la direction du joueur par rapport � l'ennemi
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Obtenir l'�chelle de l'ennemi sur l'axe des x
        float scaleX = transform.localScale.x;

        // Si l'�chelle est n�gative, inverser la direction du projectile
        if (scaleX < 0f)
        {
            direction = -direction;
        }

        // Retourner l'offset du projectile en tenant compte de la direction et de l'�chelle
        return offset.x * scaleX * Vector2.right + offset.y * Vector2.up;
    }

    private IEnumerator DestroyProjectileAfterDelay(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (projectile != null)
        {
            Destroy(projectile);
        }
    }

    private IEnumerator ResetFireTrigger(float duration)
    {
        // Attendre la dur�e de l'animation "Fire"
        yield return new WaitForSeconds(duration);

        // R�initialiser le d�clencheur "Fire" pour permettre de nouvelles animations de tir
        if (enemyAnimator != null)
        {
            enemyAnimator.ResetTrigger("Fire");
        }
    }
}