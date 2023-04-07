using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ApplicationManager;

/// <summary>
/// FOnction qui permettent de parler aux bons managers afin de quitter, retourner au menu, etc
/// </summary>
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Continue()
    {
        GameManager.instance.UnPause();
    }

    public void Settings()
    {
        //Afficher les paramètres

    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        ApplicationManager.instance.SwitchScene((int)Scenes.MENU);
    }

    public void Quit()
    {
        ApplicationManager.instance.Quit();
    }
}
