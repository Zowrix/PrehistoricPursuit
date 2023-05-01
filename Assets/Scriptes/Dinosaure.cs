using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaure : MonoBehaviour

{

    private Rigidbody2D _body;
    private Animator _animator;

    private float _hDirection = 0;
    private float _vDirection = 0;



    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //_animator.SetBool("Courir",);
    }
}
