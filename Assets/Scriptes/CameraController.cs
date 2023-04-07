using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f; // Vitesse de la caméra
    private Vector3 direction; // Direction de déplacement de la caméra

    void Update()
    {
        // Récupère l'entrée clavier
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calcule la direction de la caméra en fonction de l'entrée clavier
        direction = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Déplace la caméra en fonction de la direction et de la vitesse
        transform.position += direction * cameraSpeed * Time.deltaTime;
    }
}
