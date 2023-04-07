using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Effet de caméra qui simule un fond infini
/// </summary>
public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _ParalaxFactor = 100f;

    private Material _mat;
    // Start is called before the first frame update
    void Start()
    {
        //Récuperer les components voulus
        _mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //On veut changer l'offset de la texture en fonction de la position de la cible
        _mat.mainTextureOffset = new Vector2(_target.position.x / _ParalaxFactor, _target.position.y / _ParalaxFactor);
    }
}
