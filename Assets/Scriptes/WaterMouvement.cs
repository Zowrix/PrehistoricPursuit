using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMouvement : MonoBehaviour
{
    [SerializeField] private float _rapidité;


    private Material _mat;
    private int _vitesseConstante;
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _vitesseConstante = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _mat.mainTextureOffset = new Vector2(_vitesseConstante / _rapidité, 0);
        _vitesseConstante++;
    }
}
