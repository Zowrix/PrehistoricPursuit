using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f; // Vitesse de la cam�ra
    private Vector3 direction; // Direction de d�placement de la cam�ra

    void Update()
    {
        // R�cup�re l'entr�e clavier
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calcule la direction de la cam�ra en fonction de l'entr�e clavier
        direction = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // D�place la cam�ra en fonction de la direction et de la vitesse
        transform.position += direction * cameraSpeed * Time.deltaTime;
    }
}
