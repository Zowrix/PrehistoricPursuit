using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _vitesseMarche = 1f;
    [SerializeField] private float _vitesseCourse = 5f;
    [SerializeField] private float _puissanceSaut = 7f;
    [SerializeField] private float _doubleSautPower = 8f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _presenceSol;





    private Rigidbody2D _body;

    private float _hDirection = 0;

    private bool _courir = false;
    private bool _sauter = false;
    [SerializeField] private bool _solPrincipale = false;

    [SerializeField] private bool _doubleSaut;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(_groundCheck.position, 0.25f, _presenceSol);

        _solPrincipale = collider != null;


        _hDirection = Input.GetAxisRaw("Horizontal");

        _courir = Input.GetAxisRaw("Courir") != 0;

        _sauter = Input.GetAxisRaw("Sauter") != 0;
        Debug.Log(_sauter);

        if (_hDirection != 0)
        {
            transform.localScale = new Vector3(_hDirection, 1, 1);
        }

        if (_solPrincipale && !Input.GetButtonDown("Sauter"))
        {
            _doubleSaut = false;
        }

        if (Input.GetButtonDown("Sauter"))
        {
            if (_solPrincipale)
            {
                _body.velocity = new Vector2(_body.velocity.x, _doubleSaut ? _doubleSautPower : _puissanceSaut);
                _doubleSaut = !_doubleSaut;
                Debug.Log("doubleSaut");
            }
        }

        if (Input.GetButtonDown("Sauter") && _body.velocity.y > 0f)
        {
            _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y * 0.5f);
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
