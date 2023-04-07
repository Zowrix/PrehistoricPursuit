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
    // Start is called before the first frame update
    void Start()
    {

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
