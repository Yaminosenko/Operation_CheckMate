using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitStats : MonoBehaviour
{

    public GameObject[] _lvl1Health;
    public GameObject _lvl2Health;
    public GameObject[] _lvl1Stamina;
    public GameObject[] _lvl2Stamina;
    public GameObject _lvl1Luck;
    public GameObject _lvl2Luck;

    public GameObject[] _assaultlvl;
    public GameObject[] _sniplvl;
    public GameObject[] _shotGunlvl;
    public GameObject[] _gatlinglvl;

    public GameObject _assault;
    public GameObject _snip;
    public GameObject _shotGun;
    public GameObject _gatling;

    public int _maxPtStats = 16;
    public int _currentPtStats = 16;


    public float _healthPoint = 0;
    public float _stamina = 0;
    public float _aim = 0;
    public float _luck = 0;
    public float _weapon = 1;

    [SerializeField] private TextMeshProUGUI _number;
    private string _numberString;

    private void Update() //répicatulatife des points restants
    {
        _numberString = _currentPtStats.ToString();
        _number.SetText(_numberString);

        EquipementSysteme();
    }

    private void EquipementSysteme()
    {
        if (_healthPoint >= 3)
        {
            for (int i = 0; i < _lvl1Health.Length; i++)
            {
                _lvl1Health[i].SetActive(true);
            }
            _lvl2Health.SetActive(false);
        }
        if (_healthPoint == 6)
        {
            _lvl2Health.SetActive(true);
        }
        if (_healthPoint < 3)
        {
            for (int i = 0; i < _lvl1Health.Length; i++)
            {
                _lvl1Health[i].SetActive(false);
            }
            _lvl2Health.SetActive(false);
        }

        if(_stamina >= 3)
        {
            for (int i = 0; i < _lvl1Stamina.Length; i++)
            {
                _lvl1Stamina[i].SetActive(true);
            }
            for (int i = 0; i < _lvl2Stamina.Length; i++)
            {
                _lvl2Stamina[i].SetActive(false);
            }
        }
        if(_stamina == 6)
        {
            for (int i = 0; i < _lvl2Stamina.Length; i++)
            {
                _lvl2Stamina[i].SetActive(true);
            }
        }
        if(_stamina < 3)
        {
            for (int i = 0; i < _lvl1Stamina.Length; i++)
            {
                _lvl1Stamina[i].SetActive(false);
            }
            for (int i = 0; i < _lvl2Stamina.Length; i++)
            {
                _lvl2Stamina[i].SetActive(false);
            }
        }

        if(_luck >= 3)
        {
            _lvl1Luck.SetActive(true);
            _lvl2Luck.SetActive(false);
        }
        if(_luck == 6)
        {
            _lvl2Luck.SetActive(true);
        }
        if(_luck < 3)
        {
            _lvl1Luck.SetActive(false);
            _lvl2Luck.SetActive(false);
        }

        if(_weapon == 1)
        {
            _assault.SetActive(true);
            _snip.SetActive(false);
            _shotGun.SetActive(false);
            _gatling.SetActive(false);
        }
        else if (_weapon == 2)
        {
            _assault.SetActive(false);
            _snip.SetActive(true);
            _shotGun.SetActive(false);
            _gatling.SetActive(false);
        }
        else if (_weapon == 3)
        {
            _assault.SetActive(false);
            _snip.SetActive(false);
            _shotGun.SetActive(true);
            _gatling.SetActive(false);
        }
        else if (_weapon == 4)
        {
            _assault.SetActive(false);
            _snip.SetActive(false);
            _shotGun.SetActive(false);
            _gatling.SetActive(true);
        }

        if(_aim >= 3 && _aim != 6)
        {
            _assaultlvl[0].SetActive(false);
            _sniplvl[0].SetActive(false);
            _shotGunlvl[0].SetActive(false);
            _gatlinglvl[0].SetActive(false);

            _assaultlvl[1].SetActive(true);
            _sniplvl[1].SetActive(true);
            _shotGunlvl[1].SetActive(true);
            _gatlinglvl[1].SetActive(true);

            _assaultlvl[2].SetActive(false);
            _sniplvl[2].SetActive(false);
            _shotGunlvl[2].SetActive(false);
            _gatlinglvl[2].SetActive(false);
        }
        if (_aim == 6)
        {
            _assaultlvl[0].SetActive(false);
            _sniplvl[0].SetActive(false);
            _shotGunlvl[0].SetActive(false);
            _gatlinglvl[0].SetActive(false);

            _assaultlvl[1].SetActive(false);
            _sniplvl[1].SetActive(false);
            _shotGunlvl[1].SetActive(false);
            _gatlinglvl[1].SetActive(false);

            _assaultlvl[2].SetActive(true);
            _sniplvl[2].SetActive(true);
            _shotGunlvl[2].SetActive(true);
            _gatlinglvl[2].SetActive(true);
        }
        if(_aim < 3)
        {
            _assaultlvl[0].SetActive(true);
            _sniplvl[0].SetActive(true);
            _shotGunlvl[0].SetActive(true);
            _gatlinglvl[0].SetActive(true);

            _assaultlvl[1].SetActive(false);
            _sniplvl[1].SetActive(false);
            _shotGunlvl[1].SetActive(false);
            _gatlinglvl[1].SetActive(false);

            _assaultlvl[2].SetActive(false);
            _sniplvl[2].SetActive(false);
            _shotGunlvl[2].SetActive(false);
            _gatlinglvl[2].SetActive(false);
        }
    }
}


