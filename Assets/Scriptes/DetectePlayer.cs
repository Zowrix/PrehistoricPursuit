using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectePlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public float detectionRange = 30f;
    public float fireInterval = 2f;
    private float fireTimer = 0f;
    public GameObject projectilePrefab; // Ajout de la référence au prefab du projectile
    public float projectileSpeed = 5f;
    public Vector2 offset;
    private Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Vérifiez si le joueur est dans la plage de détection
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < detectionRange)
        {
            // Vérifiez si le délai de tir est écoulé
            if (fireTimer <= 0f)
            {
                Fire(); // Fonction pour tirer
                fireTimer = fireInterval; // Réinitialiser le délai de tir
            }
            else
            {
                fireTimer -= Time.deltaTime; // Décrémenter le délai de tir
            }
        }
        // Tourner le sprite de l'ennemi pour faire face au joueur
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Faire face à gauche
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1); // Faire face à droite
            }
        }
    }

    private void Fire()
    {
        // Créer un projectile à partir de l'ennemi avec le décalage de position
        GameObject projectile = Instantiate(projectilePrefab, (Vector2)transform.position + GetProjectileOffset(), Quaternion.identity);

        // Désactiver la gravité du projectile
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.gravityScale = 0f;

        // Calculer la direction vers le joueur
        Vector2 direction = (player.transform.position - projectile.transform.position).normalized;

        // Appliquer une force au projectile pour le déplacer dans la direction du joueur
        projectileRigidbody.velocity = direction * projectileSpeed;

        // Détruire le projectile après un laps de temps spécifié
        StartCoroutine(DestroyProjectileAfterDelay(projectile, 3f));

        // Déclencher l'animation avant le tir
        if (enemyAnimator != null)
        {
            // Déclencher l'animation "Fire"
            enemyAnimator.SetTrigger("Fire");

            // Obtenir la durée totale de l'animation "Fire"
            float animationDuration = enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            // Démarrer une coroutine pour réinitialiser le déclencheur "Fire" après la durée de l'animation
            StartCoroutine(ResetFireTrigger(animationDuration));
        }
    }

    private Vector2 GetProjectileOffset()
    {
        // Obtenir la direction du joueur par rapport à l'ennemi
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Obtenir l'échelle de l'ennemi sur l'axe des x
        float scaleX = transform.localScale.x;

        // Si l'échelle est négative, inverser la direction du projectile
        if (scaleX < 0f)
        {
            direction = -direction;
        }

        // Retourner l'offset du projectile en tenant compte de la direction et de l'échelle
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
        // Attendre la durée de l'animation "Fire"
        yield return new WaitForSeconds(duration);

        // Réinitialiser le déclencheur "Fire" pour permettre de nouvelles animations de tir
        if (enemyAnimator != null)
        {
            enemyAnimator.ResetTrigger("Fire");
        }
    }
}