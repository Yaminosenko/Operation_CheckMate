using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseComp : MonoBehaviour
{
     [SerializeField] private Button _active;
     [SerializeField] private Button _shoot;
     [SerializeField] private Button _overwatch;
     [SerializeField] private Button _defense;
     [SerializeField] private Button _grenade;
     [SerializeField] private Button _reload;
     [SerializeField] private GameObject _TabInformation;
     [SerializeField] private GameObject _percents;

     public Competance _data;

    private GameObject _canvas;
    private CurrentUnits _scriptCurrent;
     private Sprite _sprite;
     private int _index;
     private int _indexOfComp;
     private int _indexUse;


    private void OnEnable()
    {
        _canvas = GameObject.Find("Canvas");
        _scriptCurrent = _canvas.GetComponent<CurrentUnits>();
        _active.onClick.AddListener(Active);
        _shoot.onClick.AddListener(Shoot);
        _overwatch.onClick.AddListener(Overwatch);
        _defense.onClick.AddListener(Defense);
        _grenade.onClick.AddListener(Grenade);
        _reload.onClick.AddListener(Reload);
    }

    private void Update()
    {
        _sprite = _data.Icone;
        _index = _data.Index;

        _active.image.sprite = _sprite;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Shoot();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Overwatch();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Defense();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Grenade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Active();
        }
    }

    private void Shoot()
    {
         
        _indexOfComp = 1;
        AnyButtonDown();
    }

    private void Overwatch()
    {
        _indexOfComp = 2;
        AnyButtonDown();
    }

    private void Defense()
    {
        _indexOfComp = 3;
        AnyButtonDown();
    }

    private void Grenade()
    {
        //seulement lorsque il a encore une grenade
        _indexOfComp = 4;
        AnyButtonDown();
    }

    private void Reload()
    {
        //seulement quand il lui manque au moins une munition
        _indexOfComp = 5;
        AnyButtonDown();
    }

    private void Active()
    {
        _indexOfComp = 6;
        AnyButtonDown();
    }

    private void AnyButtonDown()
    {
        if (_indexOfComp != _indexUse)
        {
            if(_indexOfComp == 1)
            {
                _percents.SetActive(true);
                _scriptCurrent._isAiming = true;
            }
            else
            {
                _percents.SetActive(false);
                _scriptCurrent._isAiming = true;
            }
            _indexUse = _indexOfComp;
            _TabInformation.SetActive(true);
        }
        else
        {
            _indexUse = 0;
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._isAiming = false;
        }
    }

}
