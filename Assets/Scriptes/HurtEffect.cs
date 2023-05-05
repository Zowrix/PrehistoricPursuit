using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    [SerializeField] private bool _oneShot = false;

    [SerializeField] private GameObject _Coeur1;
    [SerializeField] private GameObject _Coeur2;
    [SerializeField] private GameObject _Coeur3;

    private static bool _canBeHurt = true; // variable partagée entre tous les scripts HurtEffect
    private static float _lastHurtTime = -1f; // temps depuis le dernier touché

    private IEnumerator WaitForHurt()
    {
        yield return new WaitForSeconds(1f);
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
            }
            else if (_Coeur2.activeSelf)
            {
                _Coeur2.SetActive(false);
            }
            else if (_Coeur3.activeSelf)
            {
                _Coeur3.SetActive(false);
            }

            _canBeHurt = false;
            StartCoroutine(WaitForHurt());
        }
        else if (collision.CompareTag("Player") && _oneShot)
        {
            Debug.Log("test1");
            _Coeur1.SetActive(false);
            _Coeur2.SetActive(false);
            _Coeur3.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _oneShot)
        {
            _Coeur1.SetActive(false);
            _Coeur2.SetActive(false);
            _Coeur3.SetActive(false);
        }
    }
}