using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentUnits : MonoBehaviour
{
    [SerializeField] private Units _data1;
    [SerializeField] private Units _data2;
    [SerializeField] private Units _data3;
    [SerializeField] private Units _data4;

    [SerializeField] private GameObject[] _units;

    private Units _dataUse;
    [SerializeField] private GameObject _active;
    private BaseComp _compScript;
    private int _index = 1;

    public bool _isAiming = false;


    private void OnEnable()
    {
        _compScript = _active.GetComponent<BaseComp>();
        _dataUse = _data1;
        _compScript._data = _dataUse.Data;
        FieldOfView _fovScript;
        _fovScript = _units[1].GetComponent<FieldOfView>();
        _fovScript._isActive = true;
    }

    private void Update()
    {
        if(_index == 1)
        {
            _dataUse = _data1;
        }

        else if(_index == 2)
        {
            _dataUse = _data2;
        }

        else if (_index == 3)
        {
            _dataUse = _data3;
        }

        else if (_index == 4)
        {
            _dataUse = _data4;
        }

        if (_isAiming == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ChangeUnits();
                SelectUnitOnTab();
                _compScript._data = _dataUse.Data;
            }
        }
    }

    private void ChangeUnits()
    {
        _index += 1;

        if (_index > 4)
        {
            _index = 1;
        }
    }

   void SelectUnitOnTab()
    {
        for (int i =0; i < _units.Length; i++)
        {
            FieldOfView _script;
            Controller _theOne;
            _script = _units[i].GetComponent<FieldOfView>();
            _theOne = _units[i].GetComponent<Controller>();

            if(i == _index - 1)
            {
                _script._isActive = true;
                _theOne._isActive = true;
            }
            else
            {
                _script._isActive = false;
                _theOne._isActive = false;
            }
        }
    }

}
