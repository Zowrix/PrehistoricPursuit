using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private string _rfidId;

    [SerializeField] private float _vitesseMarche = 5f;
    [SerializeField] private float _vitesseCourse = 5f;
    [SerializeField] private float _puissanceSaut = 7f;
    [SerializeField] private float _vitesseSlide = 5f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _presenceSol;

    [SerializeField] private Transform _wallCheck;
    [SerializeField] private LayerMask _presenceMur;

    private Rigidbody2D _body;

    private float _hDirection = 0;
    private float _vDirection = 0;

    private bool _courir = false;
    private bool _grimper = false;
    [SerializeField] private bool _solPrincipale = false;
    [SerializeField] private bool _MurGrimper = false;
    [SerializeField] private bool _doubleSaut = false;
    [SerializeField] private bool _canjump = true;
    [SerializeField] private bool _glisser = false;

    [Header("Champignon")]
    [SerializeField] private float _puissanceChampignon = 15f;
    [SerializeField] private LayerMask _champignonCheck;
    [SerializeField] private bool _champignon = false;

    public delegate void OnPlayerTouchingMushroom();
    public static event OnPlayerTouchingMushroom onPlayerTouchingMushroom;
    public delegate void OnPlayerExitMushroom();
    public static event OnPlayerExitMushroom onPlayerExitMushroom;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        ReadRFID();
    }


    public void ReadRFID()
    {
        _rfidId = "";

        LancementenFonctionDuId(_rfidId);

        Debug.Log("ID RFID: " + _rfidId);
    }

    public void LancementenFonctionDuId(string id)
    {
        if (id == " 69 231 137 172")
        {
            Debug.Log("T'es gros");
        }
        else
        {
            Debug.Log(id);
        }
    }

    // Update is called once per frame
    void Update()
    {


        Collider2D collider = Physics2D.OverlapCircle(_groundCheck.position, 0.25f, _presenceSol);

        _solPrincipale = collider != null;

        Collider2D colliderGrimper = Physics2D.OverlapCircle(_wallCheck.position, 0.25f, _presenceMur);

        _MurGrimper = colliderGrimper != null;


        Collider2D colliderChampignon = Physics2D.OverlapCircle(_groundCheck.position, 0.25f, _champignonCheck);

        _champignon = colliderChampignon != null;

        _hDirection = Input.GetAxisRaw("Horizontal");
        _vDirection = Input.GetAxis("Vertical");


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

            if (_doubleSaut)
            {
                _doubleSaut = false;
                _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);

            }
        }

        if (_champignon)
        {
            _canjump = false;
            _doubleSaut = true;
            _body.velocity = new Vector2(_body.velocity.x, _puissanceChampignon);
            onPlayerTouchingMushroom?.Invoke();
        }
        else
        {
            onPlayerExitMushroom?.Invoke();
        }

        if (_MurGrimper && !_solPrincipale)
        {
            _glisser = true;
        }
        else
        {
            _glisser = false;
        }

        if (_glisser)
        {
            _body.velocity = new Vector2(0, _vitesseSlide * _vDirection);
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



        if (_solPrincipale)
        {
            _canjump = true;
        }



    }

}
