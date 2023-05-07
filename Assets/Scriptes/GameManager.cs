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
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR

        return;
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
