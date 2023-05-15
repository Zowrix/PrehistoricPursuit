using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPosition : MonoBehaviour
{
    public GameObject _hero;

    public GameObject _cible;

    public bool _canSwitch = false;

    public bool _rfidSwitch = false;

    public void LancementenFonctionDuId(string id)
    {
        if (id == " 245 251 137 172")
        {
            _rfidSwitch = true;
        }
    }

    public void SwitchPlayerPosition(GameObject enemy)
    {

        GameObject _clickedEnemy = enemy;

        Vector3 _temPos = _hero.transform.position;
        _hero.transform.position = _clickedEnemy.transform.position;
        _clickedEnemy.transform.position = _temPos;

    }

    void OnMouseDown()
    {
        if (_rfidSwitch)
        {
            GameObject _clickedEnemy = gameObject;

            SwitchPlayerPosition(_clickedEnemy);
        }
    }

    void Update()
    {
        if (_rfidSwitch)
        {
            _canSwitch = true;

            _cible.SetActive(true);

        }

        Debug.Log("Can" + _rfidSwitch);
    }

}
