using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using WanzyeeStudio;

public class ApplicationManager : BaseSingleton<ApplicationManager>
{
    [SerializeField] private int _targetFramerate = 60;
    public enum Scenes
    {
        MENU = 0,
        GAME = 1,
        GAMEOVER = 2

    }


    void Start()
    {
        //Affecter le FPS
        Application.targetFrameRate = _targetFramerate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchScene(int sceneBuildIndex)
    {

        //     if (sceneBuildIndex == (int)Scenes.MENU)
        //   {
        //         Destroy(ApplicationManager.instance.gameObject);


        //     }
        //   else if (sceneBuildIndex == (int)Scenes.GAME)
        // {
        //   Destroy(GameManager.instance.gameObject);
        //}
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
#endif
        Application.Quit();
#if UNITY_EDITOR
        }
#endif
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
