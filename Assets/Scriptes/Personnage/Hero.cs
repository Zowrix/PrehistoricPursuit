using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _vitesseMarche = 5f;
    [SerializeField] private float _vitesseCourse = 5f;
    [SerializeField] private float _puissanceSaut = 7f;
    [SerializeField] private float _puissanceGrimpe = 10f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _presenceSol;

    [SerializeField] private Transform _wallCheck;
    [SerializeField] private LayerMask _presenceMur;



    private Rigidbody2D _body;

    private float _hDirection = 0;

    private bool _courir = false;
    private bool _grimper = false;
    [SerializeField] private bool _solPrincipale = false;
    [SerializeField] private bool _MurGrimper = false;

    [SerializeField] private bool _doubleSaut = false;
    [SerializeField] private bool _canjump = true;



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

        Collider2D colliderMur = Physics2D.OverlapCircle(_wallCheck.position, 0.25f, _presenceMur);

        _MurGrimper = colliderMur != null;


        _hDirection = Input.GetAxisRaw("Horizontal");

        _courir = Input.GetAxisRaw("Courir") != 0;
        _grimper = Input.GetAxisRaw("Grimper") != 0;



        if (_hDirection != 0)
        {
            transform.localScale = new Vector3(_hDirection, 1, 1);
        }

        if (Input.GetButtonDown("Sauter"))
        {
            if (_canjump)
            {
                _canjump = false;
                _doubleSaut = true;
                _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);
            }
            else if (_doubleSaut)
            {
                _doubleSaut = false;
                _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);

            }

            if (_solPrincipale)
            {
                _canjump = true;
            }
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

        if (_MurGrimper && _grimper)
        {
            Debug.Log("marche");
        }





    }

}
