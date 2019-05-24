using System.Collections;
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
    private Weapon _weaponUse;
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
            Debug.Log("aaa");
            PercentsCalcul();
        }
        
    }

    private void ShootButton()
    {
         
        _indexOfComp = 1;
        _tabInfoText.SetText(_shootTxt);
        AnyButtonDown();
    }

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

    public void Shoot()
    {
        Transform _target;
        Player _player;
        _target = _currentFov._actualTarget;
        _player = _target.GetComponent<Player>();
        _dmg = _weaponUse.Damage;
        if (_player != null)
        {
            _player.TakeDmg(_dmg);
        }

    }

    public void PercentsCalcul()
    {
        float _distanceTarg = _currentFov._distanceTarget;

        //for (int i = 0; i < 5; i++)
        //{
        //    if (_distanceTarg <= i * _equilibreDataPercents.PalierDistance)
        //    {
        //        _distanceTargetPercent = 0 + _equilibreDataPercents.PalierDistance * i;
        //    }
        //    else
        //    {
        //        _distanceTargetPercent = 0 + _equilibreDataPercents.PalierDistance * i;
        //    }
        //}

        //if (_distanceTarg <= _equilibreDataPercents.PalierDistance)
        //{
        //    _distanceTargetPercent = 0;
        //}
        //else if (_distanceTarg >= _equilibreDataPercents.PalierDistance && _distanceTarg <= _equilibreDataPercents.PalierDistance * 2)
        //{
        //    _distanceTargetPercent = 30;
        //}
        //else if (_distanceTarg >= _equilibreDataPercents.PalierDistance && _distanceTarg * 1 <= _equilibreDataPercents.PalierDistance * 3)
        //{
        //    _distanceTargetPercent = 60;
        //}
        //else if (_distanceTarg >= _equilibreDataPercents.PalierDistance && _distanceTarg * 2 <= _equilibreDataPercents.PalierDistance * 4)
        //{
        //    _distanceTargetPercent = 90;
        //}


        _scope = _scriptCurrent._weaponData.Scope;
        _percentsFinal = _scope - _distanceTarg ;
        _currentFov._swap = false;
    }


}
