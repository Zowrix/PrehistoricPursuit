using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPosition : MonoBehaviour
{
    public GameObject _hero;

    public GameObject _cible;

    private bool _canSwitch = false;
    public void SwitchPlayerPosition(GameObject enemy)
    {
        if (_canSwitch)
        {
            GameObject _clickedEnemy = enemy;

            Vector3 _temPos = _hero.transform.position;
            _hero.transform.position = _clickedEnemy.transform.position;
            _clickedEnemy.transform.position = _temPos;
        }
    }

    void OnMouseDown()
    {
        if (_canSwitch)
        {
            GameObject _clickedEnemy = gameObject;

            SwitchPlayerPosition(_clickedEnemy);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _canSwitch = !_canSwitch;

            if (_canSwitch)
            {
                _cible.SetActive(true);
            }
            else
            {
                _cible.SetActive(false);
            }
        }
    }
}
