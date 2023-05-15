using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apparenceBtn : MonoBehaviour
{
    // Start is called before the first frame update
    private const string PREFS_INITIALIZED_KEY = "PrefsInitialized";
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(PREFS_INITIALIZED_KEY))
        {
            gameObject.SetActive(false);
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
