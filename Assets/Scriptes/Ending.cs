using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ending : MonoBehaviour
{

    [SerializeField] private GameObject _UIEnding;

    private bool _isInterfaceOpen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("`truc en contact");
        // Vérifier si l'objet avec lequel nous sommes entrés en collision a un certain tag.
        if (other.CompareTag("Player") && !_isInterfaceOpen)
        {
            _isInterfaceOpen = true;

            _UIEnding.SetActive(true);
        }
    }
}
