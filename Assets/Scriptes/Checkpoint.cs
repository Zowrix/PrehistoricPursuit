using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // sauvegarde la position du joueur
            PlayerPrefs.SetFloat("PlayerX", other.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", other.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", other.transform.position.z);

            // active l'animation du spritesheet
            _animator.SetBool("IsCheckPointChecked", true);
        }
    }
}