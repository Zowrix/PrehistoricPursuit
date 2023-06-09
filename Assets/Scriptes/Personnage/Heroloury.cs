using System.Collections;
using System.Collections.Generic;
using static HurtEffect;
using UnityEngine;
using Cinemachine;

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
    [SerializeField] private float _tailleRetrecie = 0.5f;
    [SerializeField] private GameObject _toucheR;
    [SerializeField] private GameObject _space;
    [SerializeField] private GameObject _posiChangeUI;




    [Header("Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _presenceSol;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private LayerMask _presenceMur;
    [SerializeField] private Transform _rebordCheck;
    [SerializeField] private LayerMask _presenceRebord;
    [SerializeField] private LayerMask _plateformeMouvante;

    [Header("Vie")]
    [SerializeField] private GameObject _Coeur1;
    [SerializeField] private GameObject _Coeur2;
    [SerializeField] private GameObject _Coeur3;
    [SerializeField] private GameObject _objectCheckGround;
    [SerializeField] private GameObject _objectCheckWall;
    [SerializeField] private GameObject _objectCheckRebord;


    [Header("rebord")]
    public float jumpDistance = 1.1f;
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

    [Header("Camera")]
    [SerializeField] private GameObject _camera;

    [SerializeField] private bool _joueurSurPlateforme = false;
    private bool _courir = false;
    private bool _grimper = false;
    private bool _rebord = false;
    public bool _test = false;
    public bool _test2 = false;
    public static bool _doubleSautRFID = false;
    public static bool _miniMoiRFID = false;
    private bool _solPrincipale = false;
    private bool _detectionMur = false;
    private bool _isDead = false;
    private float _lastHDirection;
    private bool _MurGrimper = false;
    private bool _doubleSaut = false;
    private bool _canjump = true;
    private bool _glisser = false;
    private bool _retreci = false;
    public static bool _rfidSwitchHero = false;
    private bool _isPlayerCanMove = true;
    private bool _canRespawn = false;
    public static Animator animator;
    public static SpriteRenderer SpriteHero;
    public SwitchPosition switchPosition;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _contact = GetComponent<Collider2D>();
        SpriteHero = GetComponent<SpriteRenderer>();
    }



    public void LancementenFonctionDuId(string id)
    {
        if (id == " 69 231 137 172")
        {
            if (_doubleSautRFID == false)
            {
                _doubleSautRFID = true;
                Debug.Log("doublesaut");

                _miniMoiRFID = false;
                _retreci = false;
                _rfidSwitchHero = false;

                _toucheR.SetActive(false);
                _space.SetActive(true);
                _posiChangeUI.SetActive(false);

            }
        }
        else if (id == " 53 222 161 172")
        {
            if (!_miniMoiRFID)
            {
                _miniMoiRFID = true;
                Debug.Log("minimoi");
                _rfidSwitchHero = false;
                _doubleSautRFID = false;
                _toucheR.SetActive(true);
                _posiChangeUI.SetActive(false);
                _space.SetActive(false);
            }

        }
        else if (id == " 162 82 121 26")
        {
            _doubleSautRFID = false;
            _miniMoiRFID = false;
            _rfidSwitchHero = false;
            _retreci = false;

            Debug.Log("descativé");
            _posiChangeUI.SetActive(false);
            _space.SetActive(false);
            _toucheR.SetActive(false);
        }
        else if (id == " 245 251 137 172")
        {
            _rfidSwitchHero = true;
            _retreci = false;
            _doubleSautRFID = false;
            _miniMoiRFID = false;

            _posiChangeUI.SetActive(true);
            _space.SetActive(false);
            _toucheR.SetActive(false);
            Debug.Log("Je fonctionne");

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

        if (!_retreci && _objectCheckWall != null)
        {
            Collider2D colliderGrimper = Physics2D.OverlapCircle(_wallCheck.position, 0.25f, _presenceMur);

            _MurGrimper = colliderGrimper != null;
        }
        else if (_objectCheckWall != null)
        {
            Collider2D colliderGrimper = Physics2D.OverlapCircle(_wallCheck.position, 0.05f, _presenceMur);

            _MurGrimper = colliderGrimper != null;
        }

        if (!_retreci && _objectCheckWall != null)
        {
            Collider2D colliderGrimper = Physics2D.OverlapCircle(_wallCheck.position, 0.25f, _presenceSol);
            _detectionMur = colliderGrimper != null;
        }
        else if (_objectCheckWall != null)
        {
            Collider2D colliderGrimper = Physics2D.OverlapCircle(_wallCheck.position, 0.05f, _presenceSol);
            _detectionMur = colliderGrimper != null;
        }

        if (_test)
        {
            _doubleSautRFID = true;
            _miniMoiRFID = false;

        }

        if (_test2)
        {
            _doubleSautRFID = false;
            _miniMoiRFID = true;
            _rebord = false;
        }



        Collider2D colliderChampignon = Physics2D.OverlapCircle(_groundCheck.position, 0.25f, _champignonCheck);

        _champignon = colliderChampignon != null;

        if (!_retreci && _objectCheckRebord != null)
        {
            Collider2D colliderMonter = Physics2D.OverlapCircle(_rebordCheck.position, 0.25f, _presenceSol);
            _rebord = colliderMonter != null;
        }
        else if (_objectCheckRebord != null)
        {
            Collider2D colliderMonter = Physics2D.OverlapCircle(_rebordCheck.position, 0.05f, _presenceSol);
            _rebord = colliderMonter != null;
        }



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

        if (_joueurSurPlateforme)
        {
            animator.SetBool("plateforme", true);
        }
        else
        {
            animator.SetBool("plateforme", false);
        }

        _hDirection = Input.GetAxisRaw("Horizontal");
        _vDirection = Input.GetAxis("Vertical");


        _courir = Input.GetAxisRaw("Courir") != 0;
        _grimper = Input.GetAxisRaw("Grimper") != 0;
        if (_isPlayerCanMove)
        {

            if (_hDirection != 0 && !_retreci)
            {
                transform.localScale = new Vector3(_hDirection, _tailleNormale, 1);
                _lastHDirection = _hDirection;
            }
            else if (_hDirection != 0 && _retreci)
            {
                transform.localScale = new Vector3(_hDirection * _tailleRetrecie, _tailleRetrecie, 1f);
                _lastHDirection = _hDirection;
            }



            if (Input.GetButtonDown("Sauter"))
            {

                if (_canjump && !_doubleSautRFID && (_solPrincipale || _joueurSurPlateforme))
                {

                    _canjump = false;
                    _solPrincipale = false;
                    _body.velocity = new Vector2(_body.velocity.x, _puissanceSaut);
                }

                if (_doubleSautRFID)
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
                    _retreci = !_retreci;
                }
            }

            //Monter automatiquement les trottoirs

            if (_solPrincipale && _rebord && !_detectionMur && !_isDead && !_miniMoiRFID)
            {
                transform.position = new Vector2(transform.position.x + _lastHDirection / 1.5f, transform.position.y + (jumpDistance * transform.localScale.y));
            }



            if (!_Coeur1.activeSelf && !_Coeur2.activeSelf && !_Coeur3.activeSelf && _contact.enabled == true)
            {
                _isDead = true;
                _body.velocity = new Vector2(_body.velocity.x, _puissanceChampignon);
                _contact.enabled = false;
                _canRespawn = true;
                _camera.GetComponent<CinemachineVirtualCamera>().Follow = null;
            }

            if (_isDead && _canRespawn)
            {
                _canRespawn = false;
                StartCoroutine(Respawn(5f));
            }

            //Animation

            if (_doubleSautRFID)
            {
                animator.SetBool("MarcherDoubleSaut", _hDirection != 0 && !_courir);
                animator.SetBool("CourirDoubleSaut", _hDirection != 0 && _courir);
                animator.SetBool("IdleDoubleSaut", true);
                animator.SetBool("Marcher", false);
                animator.SetBool("Courir", false);


            }
            else
            {
                animator.SetBool("Marcher", _hDirection != 0 && !_courir);
                animator.SetBool("Courir", _hDirection != 0 && _courir);
                animator.SetBool("IdleDoubleSaut", false);

            }




            animator.SetFloat("VelocityY", _body.velocity.y);
            animator.SetBool("Sol", _solPrincipale);

        }
        else if (_doubleSautRFID)
        {
            animator.SetBool("MarcherDoubleSaut", false);
            animator.SetBool("CourirDoubleSaut", false);
            animator.SetBool("IdleDoubleSaut", true);
        }
        else
        {

            animator.SetBool("Marcher", false);
            animator.SetBool("Courir", false);
        }

    }

    private IEnumerator Respawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpriteHero.material.color = Color.white;
        _canBeHurt = true;
        animator.SetBool("isPersoDead", false);
        SpriteHero.sortingOrder -= 150;
        _isDead = false;

        _Coeur1.SetActive(true);
        _Coeur2.SetActive(true);
        _Coeur3.SetActive(true);
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");
        float playerZ = PlayerPrefs.GetFloat("PlayerZ");
        transform.position = new Vector3(playerX, playerY, playerZ);
        _body.velocity = new Vector3(0, 0, 0);
        _contact.enabled = true;
        _camera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
    }
    private void FixedUpdate()
    {
        if (_isPlayerCanMove)
        {
            if (!_courir && !_isDead)
            {
                _body.velocity = new Vector2(_vitesseMarche * _hDirection, _body.velocity.y);
            }
            else if (!_isDead)
            {
                _body.velocity = new Vector2(_vitesseCourse * _hDirection, _body.velocity.y);

            }

            if (_retreci)
            {
                transform.localScale = new Vector3(_tailleRetrecie, _tailleRetrecie, 1f);

            }
            else
            {
                transform.localScale = new Vector3(_tailleNormale, _tailleNormale, 1f);
            }

            if (_solPrincipale || _joueurSurPlateforme)
            {
                _canjump = true;

            }
        }
        else
        {

            _body.velocity = new Vector2(0, 0);
        }
    }

    public void ReceivedataCam(bool receivedValue)
    {
        _isPlayerCanMove = receivedValue;
    }

}
