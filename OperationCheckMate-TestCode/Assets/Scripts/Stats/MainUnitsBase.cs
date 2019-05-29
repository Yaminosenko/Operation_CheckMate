using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUnitsBase : MonoBehaviour
{

    [SerializeField] public Button _next;
    public UnitStats[] _units;
    public Units[] _dataBase;

    private void OnEnable()
    {

        _next.onClick.AddListener(UpdateStats);
       // InvokeRepeating("UpdateStats", 0f, 0.5f);
    }

    private void Update()
    {
        
    }

    private void UpdateStats()
    {
        
        for (int i = 0; i < 4; i++)
        {
            _dataBase[i].Health = _units[i]._healthPoint;
            _dataBase[i].Stamina = _units[i]._stamina;
            _dataBase[i].Aim = _units[i]._aim;
            _dataBase[i].Luck = _units[i]._luck;
        }


        
        StaticStats1.Health = _units[0]._healthPoint;
        StaticStats1.Stamina = _units[0]._stamina;
        StaticStats1.Aim = _units[0]._aim;
        StaticStats1.Luck = _units[0]._luck;
        StaticStats1.Weapon = _units[0]._weapon;

        StaticStats2.Health = _units[1]._healthPoint;
        StaticStats2.Stamina = _units[1]._stamina;
        StaticStats2.Aim = _units[1]._aim;
        StaticStats2.Luck = _units[1]._luck;
        StaticStats2.Weapon = _units[1]._weapon;

        StaticStats3.Health = _units[2]._healthPoint;
        StaticStats3.Stamina = _units[2]._stamina;
        StaticStats3.Aim = _units[2]._aim;
        StaticStats3.Luck = _units[2]._luck;
        StaticStats3.Weapon = _units[2]._weapon;

        StaticStats4.Health = _units[3]._healthPoint;
        StaticStats4.Stamina = _units[3]._stamina;
        StaticStats4.Aim = _units[3]._aim;
        StaticStats4.Luck = _units[3]._luck;
        StaticStats4.Weapon = _units[3]._weapon;

        SceneManager.LoadScene("HUD", LoadSceneMode.Single);
    }
}
