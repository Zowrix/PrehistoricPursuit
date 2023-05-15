using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WanzyeeStudio;
using static ApplicationManager;

/// <summary>
/// Gestionnaire de la partie 
/// </summary>

public class GameManager : BaseSingleton<GameManager>
{


    [Header("UI")]
    [SerializeField] private GameObject _pauseMenuUI;

    [Header("Personnage")]
    public Transform player;

    private const string PREFS_INITIALIZED_KEY = "PrefsInitialized";
    // Start is called before the first frame update
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PREFS_INITIALIZED_KEY))
        {
            // Initialiser les PlayerPrefs avec les valeurs souhaitées
            PlayerPrefs.SetFloat("PlayerX", -75.40005f);
            PlayerPrefs.SetFloat("PlayerY", 0.7376772f);
            PlayerPrefs.SetFloat("PlayerZ", 0);

            // Définir la clé PREFS_INITIALIZED_KEY pour indiquer que les PlayerPrefs sont maintenant initialisés
            PlayerPrefs.SetInt(PREFS_INITIALIZED_KEY, 1);

            // Sauvegarder les modifications des PlayerPrefs
            PlayerPrefs.Save();
        }
    }


    void Start()
    {
#if UNITY_EDITOR


#else
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");
        float playerZ = PlayerPrefs.GetFloat("PlayerZ");
        player.position = new Vector3(playerX, playerY, playerZ);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        //afficher le menu pause
        _pauseMenuUI.SetActive(true);
        //Arrêter le temps
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        _pauseMenuUI.SetActive(false);
        //activer le temps
        Time.timeScale = 1;
    }

}
