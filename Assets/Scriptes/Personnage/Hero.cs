using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _vitesseMarche = 1f;
    [SerializeField] private float _vitesseCourse = 5f;


    private Rigidbody2D _body;

    private float _hDirection = 0;

    private bool _courir = false;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _hDirection = Input.GetAxisRaw("Horizontal");

        _courir = Input.GetAxisRaw("Courir") != 0;



        if (_hDirection != 0)
        {
            transform.localScale = new Vector3(_hDirection, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (!_courir)
        {
            _body.velocity = new Vector2(_vitesseMarche * _hDirection, _body.velocity.y);
        }
        else
        {
            _body.velocity = new Vector2(_vitesseCourse * _hDirection, _body.velocity.y);

        }
    }

}
