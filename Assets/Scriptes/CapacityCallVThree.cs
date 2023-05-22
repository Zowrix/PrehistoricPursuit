using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using static Heroloury;
using UnityEngine;

public class CapacityCallVThree : MonoBehaviour
{
    [SerializeField] private GameObject _UIHeroSpeak;
    public Heroloury Heroloury;
    private bool _isInterfaceOpen = false;
    private bool isActivated = false;
    private void Start()
    {

    }

    void Update()
    {
        if (_isInterfaceOpen && _rfidSwitchHero && !isActivated)
        {
            _UIHeroSpeak.SetActive(false);
            Heroloury.ReceivedataCam(true);
            isActivated = true;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("`truc en contact");
        // Vérifier si l'objet avec lequel nous sommes entrés en collision a un certain tag.
        if (other.CompareTag("Player") && !_isInterfaceOpen)
        {
            _isInterfaceOpen = true;

            _UIHeroSpeak.SetActive(true);
            Heroloury.ReceivedataCam(false);
            //Destroy(gameObject);
        }
    }
}
