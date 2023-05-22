using System.Collections;
using System.Collections.Generic;
using static Heroloury;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    [SerializeField] private bool _oneShot = false;

    [SerializeField] private GameObject _Coeur1;
    [SerializeField] private GameObject _Coeur2;
    [SerializeField] private GameObject _Coeur3;

    public static bool _canBeHurt = true; // variable partagée entre tous les scripts HurtEffect
    private static float _lastHurtTime = -1f; // temps depuis le dernier touché

    private IEnumerator WaitForHurt()
    {
        float hurtTime = 1f; // Durée du clignotement en rouge
        float blinkInterval = 0.2f; // Intervalle entre les changements de couleur
        int numBlinks = Mathf.FloorToInt(hurtTime / (2f * blinkInterval)); // Nombre de clignotements nécessaires

        // Effectue le clignotement en rouge
        for (int i = 0; i < numBlinks; i++)
        {
            SpriteHero.material.color = new Color(1f, 0.5f, 0.5f);
            yield return new WaitForSeconds(blinkInterval);
            SpriteHero.material.color = Color.white;
            yield return new WaitForSeconds(blinkInterval);
        }

        SpriteHero.color = Color.white; // Revenu à la couleur blanche
        _canBeHurt = true;
        _lastHurtTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test1");
        if (collision.CompareTag("Player") && _canBeHurt && (Time.time - _lastHurtTime >= 1f) && !_oneShot)
        {
            Debug.Log("test1");
            if (_Coeur1.activeSelf)
            {
                _Coeur1.SetActive(false);
                SpriteHero.material.color = new Color(1f, 0.5f, 0.5f);

                StartCoroutine(WaitForHurt());

            }
            else if (_Coeur2.activeSelf)
            {
                _Coeur2.SetActive(false);

                StartCoroutine(WaitForHurt());
            }
            else if (_Coeur3.activeSelf)
            {
                _Coeur3.SetActive(false);
                SpriteHero.material.color = Color.red;
                animator.SetBool("isPersoDead", true);
                SpriteHero.sortingOrder += 150;

            }

            _canBeHurt = false;


        }
        else if (collision.CompareTag("Player") && _oneShot)
        {
            Debug.Log("test1");
            _Coeur1.SetActive(false);
            _Coeur2.SetActive(false);
            _Coeur3.SetActive(false);
            SpriteHero.sortingOrder += 150;

            SpriteHero.material.color = Color.red;
            animator.SetBool("isPersoDead", true);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _oneShot)
        {
            _Coeur1.SetActive(false);
            _Coeur2.SetActive(false);
            _Coeur3.SetActive(false);
            SpriteHero.sortingOrder += 150;

            SpriteHero.material.color = Color.red;
            animator.SetBool("isPersoDead", true);

        }
    }
}