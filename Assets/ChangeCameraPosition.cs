using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Compy;

    private bool _contactColliderCamera;
    private bool _shouldActivateBool;
    private float _timer;
    public float movementSpeed = 5f;
    private Animator AnimationCompy;
    private SpriteRenderer SpriteCompy;
    private Rigidbody2D RigidbodyCompy;
    private CinemachineVirtualCamera CameraProperties;
    [SerializeField] private Heroloury Heroloury;




    private void Start()
    {
        AnimationCompy = Compy.GetComponent<Animator>();
        SpriteCompy = Compy.GetComponent<SpriteRenderer>();
        RigidbodyCompy = Compy.GetComponent<Rigidbody2D>();
        CameraProperties = Camera.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (_contactColliderCamera)
        {
            // Démarre le compte à rebours lorsque le contact est détecté
            if (!_shouldActivateBool)
            {
                _timer += Time.deltaTime;
                if (_timer >= 2f)
                {
                    SpriteCompy.flipX = true;
                }

                if (_timer >= 4f)
                {
                    CameraProperties.Follow = null;
                    AnimationCompy.SetBool("Course", true);
                    RigidbodyCompy.velocity = new Vector2(movementSpeed, RigidbodyCompy.velocity.y);
                }
                if (_timer >= 6f)
                {
                    _shouldActivateBool = true;
                    Compy.SetActive(false);
                    Camera.SetActive(false);
                    gameObject.SetActive(false);
                    _contactColliderCamera = false;
                    Heroloury.ReceivedataCam(true);

                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("`truc en contact");
        // Vérifier si l'objet avec lequel nous sommes entrés en collision a un certain tag.
        if (other.CompareTag("Player"))
        {
            // Code à exécuter lorsque le joueur entre en collision avec ce collider.
            Debug.Log("joueur en contact");
            _contactColliderCamera = true;
            Heroloury.ReceivedataCam(false);
            Camera.SetActive(true);
        }
    }
}
