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

     public Competance _data;

     private GameObject _canvas;
     private CurrentUnits _scriptCurrent;
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

        _player.TakeDmg(3);
    }




}
