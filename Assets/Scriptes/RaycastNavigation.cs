using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Exemple de navigation simple utilisant les raycasts
/// </summary>
public class RaycastNavigation : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundRaydirection;
    [SerializeField] private Vector2 _jumpRayDirection;
    [SerializeField] private float _groundRayLength = 1f;
    [SerializeField] private float _jumpRaylength = 1f;
    [SerializeField] private float _jumpPower = 1f;
    [SerializeField] private float _speed = 1f;



    private Ray2D _navigationRay;
    private Ray2D _jumpRay;
    private Ray2D _playerRay;
    private bool _groundInFront = false;
    private Rigidbody2D _body;
    private int _movementDirection = 1;//1 right, -1 left;
    private bool _shouldJump = false;
    private bool _jumping = false;
    private bool _grounded = false;

    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        //création des ray2D au départ, seule leur posiotion changera (afin de toujour suivre le mob)
        _navigationRay = new Ray2D(transform.position, _groundRaydirection.normalized * _groundRayLength);
        _jumpRay = new Ray2D(transform.position, _jumpRayDirection.normalized * _jumpRaylength);

    }

    // Update is called once per frame
    void Update()
    {
        //update ray origin at each frame to follow mob
        _navigationRay.origin = transform.position;
        _jumpRay.origin = transform.position;
        _playerRay.origin = transform.position;

        _grounded = _collider.IsTouchingLayers(_groundLayer);



        if (_grounded)
        {
            //ground check in front of mob
            RaycastHit2D hit = Physics2D.Raycast(_navigationRay.origin, _navigationRay.direction, _groundRayLength, _groundLayer);

            //si on trouve un collider a nos pieds c'est qu'on peut encore avancer
            _groundInFront = hit.collider != null;

            if (!_groundInFront && !_shouldJump)
            {

                //check for jump, if not ground in front
                RaycastHit2D jumpHit = Physics2D.Raycast(_jumpRay.origin, _jumpRay.direction, _jumpRaylength, _groundLayer);

                if (jumpHit.collider != null)
                {
                    //a place to jump has been found
                    _shouldJump = true;
                }
                else
                {
                    //we must change direction... or we fall to our doom !
                    _groundRaydirection.x = -_groundRaydirection.x;
                    _navigationRay.direction = _groundRaydirection;
                    _movementDirection = -_movementDirection;

                    _jumpRayDirection.x = -_jumpRayDirection.x;
                    _jumpRay.direction = _jumpRayDirection;


                }
            }

            //si on touche le sol, c'est que l'on n'est pas en train de sauter
            _jumping = false;

            //voir les raycasts pour le debugging
            Debug.DrawRay(_navigationRay.origin, _navigationRay.direction * _groundRayLength, Color.red, 0.01f);
            Debug.DrawRay(_jumpRay.origin, _jumpRay.direction * _jumpRaylength, Color.green, 0.01f);
        }
    }

    private void FixedUpdate()
    {
        //on bouge le mob
        _body.velocity = new Vector2(_movementDirection * _speed, _body.velocity.y);

        //si on doit sauter
        if (_shouldJump)
        {
            _shouldJump = false;
            Vector2 jumpDirection = new Vector2(1 * _movementDirection, 1) * _jumpPower;
            _body.AddForce(jumpDirection, ForceMode2D.Impulse);
            _jumping = true;
        }
    }
}
