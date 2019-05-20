using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] public Button _weaponRight;
    [SerializeField] public Button _weaponLeft;

    [SerializeField] public Image _weaponImage;

    [SerializeField] private Sprite _assault;
    [SerializeField] private Sprite _sniper;
    [SerializeField] private Sprite _shotgun;
    [SerializeField] private Sprite _gatling;

    private int _index = 1;
    public  GameObject _units;
    private UnitStats _unitsScript;



    private void Start()
    {
        _weaponRight.onClick.AddListener(WeaponRight);
        _weaponLeft.onClick.AddListener(WeaponLeft);


        _unitsScript = _units.GetComponent<UnitStats>();
    }




    public void WeaponLeft()
    {
        _index -= 1;
        Debug.Log(_index);
        ChangeSprit();
        _unitsScript._weapon = _index;
        if (_index <= 0)
        {
            _index = 4;
        }
    }

    public void WeaponRight()
    {
        _index += 1;
        Debug.Log(_index);
        ChangeSprit();
        _unitsScript._weapon = _index;
        if (_index >= 4)
        {
            _index = 0;
        }
    }

    private void ChangeSprit()
    {
        if(_index == 1)
        {
            _weaponImage.sprite = _assault;
        }
        else if (_index == 2)
        {
            _weaponImage.sprite = _sniper;
        }
        else if (_index == 3)
        {
            _weaponImage.sprite = _shotgun;
        }
        else if (_index == 4)
        {
            _weaponImage.sprite = _gatling;
        }
    }
}
