using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroloury : MonoBehaviour
{
    [Header("Marche/course")]
    [SerializeField] private float _vitesseMarche = 5f;
    [SerializeField] private float _vitesseCourse = 5f;
    private float _hDirection = 0;
    private float _vDirection = 0;

    [SerializeField] private float _puissanceSaut = 7f;

    [Header("Pouvoir")]
    [SerializeField] private float _tailleNormale = 1f;
    [SerializeField] private float _tailleRétrécie = 0.5f;
    [SerializeField] private GameObject _toucheR;
    [Header("Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _presenceSol;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private LayerMask _presenceMur;
    [SerializeField] private Transform _rebordCheck;
    [SerializeField] private LayerMask _presenceRebord;
    [SerializeField] private LayerMask _plateformeMouvante;


    [Header("rebord")]
    public float jumpDistance = 1f;
    private Rigidbody2D _body;
    private Collider2D _contact;

    [Header("S'accrocher au mur")]
    [SerializeField] private float _vitesseSlide = 5f;

    [Header("Champignon")]
    [SerializeField] private float _puissanceChampignon = 15f;
    [SerializeField] private LayerMask _champignonCheck;
    [SerializeField] private bool _champignon = false;
    public delegate void OnPlayerTouchingMushroom();
    public static event OnPlayerTouchingMushroom onPlayerTouchingMushroom;
    public delegate void OnPlayerExitMushroom();
    public static event OnPlayerExitMushroom onPlayerExitMushroom;





    [SerializeField] private bool _joueurSurPlateforme = false;
    private bool _courir = false;
    private bool _grimper = false;
    private bool _rebord = false;
    private bool _doubleSautRFID = false;
    private bool _miniMoiRFID = false;
    private bool _solPrincipale = false;
    private bool _MurGrimper = false;
    private bool _doubleSaut = false;
    private bool _canjump = true;
    private bool _glisser = false;
    private bool _retréci = false;

    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _contact = GetComponent<Collider2D>();
    }



    public void LancementenFonctionDuId(string id)
    {
        if (id == " 69 231 137 172")
        {
            if (_doubleSautRFID == false)
            {
                _doubleSautRFID = true;
                Debug.Log(_doubleSautRFID);
            }
            else if (id == " 162 82 121 26")
            {
                if (!_miniMoiRFID)
                {
                    _miniMoiRFID = true;
                }
                else if (_miniMoiRFID && _retréci)
                {
                    _miniMoiRFID = false;
                    _retréci = false;
                }
                else
                {
                    _miniMoiRFID = false;
                }
            }
            else
            {
                _doubleSautRFID = false;
                Debug.Log(_doubleSautRFID);
            }
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

        Collider2D colliderMonter = Physics2D.OverlapCircle(_rebordCheck.position, 0.25f, _presenceSol);

        _rebord = colliderMonter != null;

        Collider2D colliderPlatform = Physics2D.OverlapCircle(_groundCheck.position, 0.25f, _plateformeMouvante);

        if (colliderPlatform != null)
        {
            transform.parent = colliderPlatform.transform;
        }
        else
        {
            transform.SetParent(null);
        }
        _joueurSurPlateforme = colliderPlatform != null;


        _hDirection = Input.GetAxisRaw("Horizontal");
        _vDirection = Input.GetAxis("Vertical");


        _courir = Input.GetAxisRaw("Courir") != 0;
        _grimper = Input.GetAxisRaw("Grimper") != 0;

        if (_hDirection != 0 && !_retréci)
        {
            transform.localScale = new Vector3(_hDirection, _tailleNormale, 1);
        }
        else if (_hDirection != 0 && _retréci)
        {
            transform.localScale = new Vector3(_hDirection * _tailleRétrécie, _tailleRétrécie, 1f);
        }

        if (Input.GetButtonDown("Sauter"))
        {
            if (_canjump && !_doubleSautRFID && (_solPrincipale || _joueurSurPlateforme))
            {
                _canjump = false;
                _solPrincipale = false;
                _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);
                Debug.Log(_canjump);
            }

            if (_doubleSautRFID)
            {
                Debug.Log("Double");

                if (_canjump)
                {
                    _canjump = false;
                    _doubleSaut = true;
                    _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);
                    Debug.Log("Je suis la");
                }

                if (_doubleSaut)
                {
                    _doubleSaut = false;
                    _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);
                    Debug.Log("Je suis la 2");
                }
            }
        }

        if (_champignon)
        {
            _canjump = false;
            if (_doubleSautRFID)
            { _doubleSaut = true; }

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

        if (_miniMoiRFID)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _retréci = !_retréci;
            }


        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _toucheR.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            _toucheR.SetActive(false);
        }

        //Monter automatiquement les trottoirs

        if (_solPrincipale && _rebord && !_MurGrimper)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (jumpDistance * transform.localScale.y));
        }
        //Animation
        _animator.SetBool("Marcher", _hDirection != 0 && !_courir);
        _animator.SetBool("Courir", _hDirection != 0 && _courir);

    }

    void AppliquerRetrecissement()
    {
        if (_retréci)
        {
            transform.localScale = new Vector3(_tailleRétrécie, _tailleRétrécie, 1f);
            _body.gravityScale = _tailleRétrécie;
        }
        else
        {
            transform.localScale = new Vector3(_tailleNormale, _tailleNormale, 1f);
            _body.gravityScale = _tailleNormale;
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

        if (_retréci)
        {
            transform.localScale = new Vector3(_tailleRétrécie, _tailleRétrécie, 1f);
        }
        else
        {
            transform.localScale = new Vector3(_tailleNormale, _tailleNormale, 1f);

        }

        if (_solPrincipale || _joueurSurPlateforme)
        {
            _canjump = true;
            Debug.Log(_solPrincipale);
        }





    }
}
