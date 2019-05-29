using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUnitsBase : MonoBehaviour
{

    [SerializeField] private Button _next;
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;
    public UnitStats[] _units;
    public Units[] _dataBase;
    public Competance[] _evryComp;

    private bool _index = false;

    private void OnEnable()
    {

        _next.onClick.AddListener(UpdateStats);
        // InvokeRepeating("UpdateStats", 0f, 0.5f);
        _player2.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void UpdateStats()
    {
        
        if (_index == true)
        {
            for (int i = 4; i < 8; i++)
            {
                _dataBase[i].Health = _units[i]._healthPoint;
                _dataBase[i].Stamina = _units[i]._stamina;
                _dataBase[i].Aim = _units[i]._aim;
                _dataBase[i].Luck = _units[i]._luck;

                if (_units[i]._weapon == 1)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.Assault;
                    _dataBase[i].Data = _evryComp[0];
                }
                else if (_units[i]._weapon == 2)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.Sniper;
                    _dataBase[i].Data = _evryComp[1];
                }
                else if (_units[i]._weapon == 3)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.ShotGun;
                    _dataBase[i].Data = _evryComp[2];
                }
                else if (_units[i]._weapon == 4)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.Gatling;
                    _dataBase[i].Data = _evryComp[3];
                }
                 Debug.Log("vroum");
            }

            SceneManager.LoadScene("HUD", LoadSceneMode.Single);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                _dataBase[i].Health = _units[i]._healthPoint;
                _dataBase[i].Stamina = _units[i]._stamina;
                _dataBase[i].Aim = _units[i]._aim;
                _dataBase[i].Luck = _units[i]._luck;

                if (_units[i]._weapon == 1)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.Assault;
                    _dataBase[i].Data = _evryComp[0];
                }
                else if (_units[i]._weapon == 2)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.Sniper;
                    _dataBase[i].Data = _evryComp[1];
                }
                else if (_units[i]._weapon == 3)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.ShotGun;
                    _dataBase[i].Data = _evryComp[2];
                }
                else if (_units[i]._weapon == 4)
                {
                    _dataBase[i].WeaponList = Units.WeaponEnum.Gatling;
                    _dataBase[i].Data = _evryComp[3];
                }
            }

            _player2.SetActive(true);
            _player1.SetActive(false);
        }

        _index = true;
    }
}
