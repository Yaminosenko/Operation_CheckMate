﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
     [SerializeField] private Percents_palier _equilibreDataPercents;

     public Competance _data;

     private GameObject _canvas;
     private CurrentUnits _scriptCurrent;
     public Weapon _weaponUse;
     private Sprite _sprite;
     private int _index;
     private int _indexOfComp;
     private int _dmg;
     

    private string _shootTxt = "Oui le tire";
    private string _ovTxt = "Oui la Vigilence";
    private string _defenseTxt = "Oui le defense";
    private string _grenadeTxt = "Oui la grenade";
    private string _reloadTxt = "Oui le Reload";
    private string _ActiveTxt = "Oui l'active";

    private TextMeshProUGUI _tabInfoText;
    public FieldOfView _currentFov;
    public Player _playerScript;

    private float _scope;
    private float _distanceTargetPercent;
    private int _protectLvl;
    [SerializeField] private float _percentsFinal;


    private void OnEnable()
    {
        _canvas = GameObject.Find("Canvas");
        _scriptCurrent = _canvas.GetComponent<CurrentUnits>();
        _active.onClick.AddListener(ActiveButton);
        _shoot.onClick.AddListener(ShootButton);
        _overwatch.onClick.AddListener(OverwatchButton);
        _defense.onClick.AddListener(DefenseButton);
        _grenade.onClick.AddListener(GrenadeButton);
        _reload.onClick.AddListener(ReloadButton);

        
        

        _tabInfoText = _TabInformation.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() 
    {
        _sprite = _data.Icone;
        _index = _data.Index;

        _active.image.sprite = _sprite;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootButton();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OverwatchButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DefenseButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GrenadeButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ReloadButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ActiveButton();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._cantSwap = false;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (_indexOfComp == 1)
            {
                Shoot();
            }
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._cantSwap = false;

        }

        if(_currentFov._swap == true)
        {
            PercentsCalcul();
        }
        
    }

    private void ShootButton()
    {
         if(_playerScript._ammo > 0)
         {
            _indexOfComp = 1;
            _tabInfoText.SetText(_shootTxt);
            AnyButtonDown();
         }
    }// tout les boutons

    private void OverwatchButton()
    {
        _indexOfComp = 2;
        _tabInfoText.SetText(_ovTxt);
        AnyButtonDown();
    }

    private void DefenseButton()
    {
        _indexOfComp = 3;
        _tabInfoText.SetText(_defenseTxt);
        AnyButtonDown();
    }

    private void GrenadeButton()
    {
        //seulement lorsque il a encore une grenade
        _indexOfComp = 4;
        _tabInfoText.SetText(_grenadeTxt);
        AnyButtonDown();
    }

    private void ReloadButton()
    {
        //seulement quand il lui manque au moins une munition
        _indexOfComp = 5;
        _tabInfoText.SetText(_reloadTxt);
        AnyButtonDown();
    }

    private void ActiveButton()
    {
        _indexOfComp = 6;
        _tabInfoText.SetText(_ActiveTxt);
        AnyButtonDown();
    }

    private void AnyButtonDown() // Affichage du txt du bouton
    {
        //if (_indexOfComp != _indexUse)
        //{
            if(_indexOfComp == 1)
            {
                _percents.SetActive(true);
                _scriptCurrent.IsAimaing(true);
                
            }
            else
            {
                _percents.SetActive(false);
                _scriptCurrent.IsAimaing(false);
            }

          
            _TabInformation.SetActive(true);
            _scriptCurrent._cantSwap = true;
        //}
        //else
        //{
        //    _indexUse = 0;
        //    _indexOfComp = 0;
        //    _TabInformation.SetActive(false);
        //    _percents.SetActive(false);
        //    _scriptCurrent._cantSwap = false;
        //}
    }


    public void Shoot() // tir sur une cible donnée
    {
        
        Transform _target;
        Player _playerTarget;
        _target = _currentFov._actualTarget;
        _playerTarget = _target.GetComponent<Player>();
        _dmg = _weaponUse.Damage;
        if (_playerTarget != null)
        {
            _playerScript._ammo -= 1;
        
            if (RandomShoot() == true)
            {
                _playerTarget.TakeDmg(_dmg);
                Debug.Log(_dmg);
                
            }
        }
    }

    public void PercentsCalcul() //calcule du pourcentage de chance de toucher la cible 
    {
        float _distanceTarg = _currentFov._distanceTarget;

        _scope = _scriptCurrent._weaponData.Scope;
        _percentsFinal = _scope - _distanceTarg ;
        _currentFov._swap = false;
    }

    public bool RandomShoot() // Choix du randome grace au pourcentage converti en palier pour une meillieurs expérience de jeu moins frustrante
    {
        float _value = 0f; 
        float _success; 
        
        if (_percentsFinal < 10f)
        {
            return false; 
        }
        else if (_percentsFinal > 10 && _percentsFinal < 25)
        {
            _value = 25f;
        }
        else if (_percentsFinal > 25 && _percentsFinal < 50)
        {
            _value = 50f;
        }
        else if (_percentsFinal > 50 && _percentsFinal < 75)
        {
            _value = 75f;
        }
        else if (_percentsFinal > 75)
        {
            return true;
        }

        _success = Random.Range(0, 100);
        Debug.Log(_success);

        if (_success < _value)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
