using System.Collections;
using System.Collections.Generic;
using static Hero;
using UnityEngine;

public class SwitchPosition : MonoBehaviour
{
    public GameObject _hero;

    public GameObject _cible;


    void Update()
    {
        Debug.Log(Hero._rfidSwitchHero);
        if (Hero._rfidSwitchHero)
        {

            Debug.Log(Hero._rfidSwitchHero);
            _cible.SetActive(true);

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
        if (Hero._rfidSwitchHero)
        {

            GameObject _clickedEnemy = gameObject;

            SwitchPlayerPosition(_clickedEnemy);
        }
    }



}
