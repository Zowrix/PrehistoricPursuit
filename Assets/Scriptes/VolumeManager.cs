using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance; // Instance unique du VolumeManager
    public float volumeLevel = 1f; // Niveau de volume global

    private void Awake()
    {
        // Assurer qu'il n'y a qu'une seule instance du VolumeManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Pour que l'objet persiste entre les scènes
    }

    public void SetGlobalVolume(float volume)
    {
        volumeLevel = volume;
        AudioListener.volume = volumeLevel;
    }

    public void UpdateSliderPosition(Slider slider)
    {
        slider.value = volumeLevel;
    }
}