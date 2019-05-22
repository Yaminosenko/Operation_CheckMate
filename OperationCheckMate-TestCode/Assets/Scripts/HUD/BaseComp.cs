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
     private int _indexUse;

    private string _shootTxt = "Oui le tire";
    private string _ovTxt = "Oui la Vigilence";
    private string _defenseTxt = "Oui le defense";
    private string _grenadeTxt = "Oui la grenade";
    private string _reloadTxt = "Oui le Reload";
    private string _ActiveTxt = "Oui l'active";

    private TextMeshProUGUI _tabInfoText;


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

        _tabInfoText = _TabInformation.GetComponentInChildren<TextMeshProUGUI>();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _indexUse = 0;
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._cantSwap = false;
        }
    }

    private void Shoot()
    {
         
        _indexOfComp = 1;
        _tabInfoText.SetText(_shootTxt);
        AnyButtonDown();
    }

    private void Overwatch()
    {
        _indexOfComp = 2;
        _tabInfoText.SetText(_ovTxt);
        AnyButtonDown();
    }

    private void Defense()
    {
        _indexOfComp = 3;
        _tabInfoText.SetText(_defenseTxt);
        AnyButtonDown();
    }

    private void Grenade()
    {
        //seulement lorsque il a encore une grenade
        _indexOfComp = 4;
        _tabInfoText.SetText(_grenadeTxt);
        AnyButtonDown();
    }

    private void Reload()
    {
        //seulement quand il lui manque au moins une munition
        _indexOfComp = 5;
        _tabInfoText.SetText(_reloadTxt);
        AnyButtonDown();
    }

    private void Active()
    {
        _indexOfComp = 6;
        _tabInfoText.SetText(_ActiveTxt);
        AnyButtonDown();
    }

    private void AnyButtonDown()
    {
        if (_indexOfComp != _indexUse)
        {
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

            _indexUse = _indexOfComp;
            _TabInformation.SetActive(true);
            _scriptCurrent._cantSwap = true;
        }
        else
        {
            _indexUse = 0;
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._cantSwap = false;
        }
    }

}
