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
    private Units _dataUse;
    [SerializeField] private GameObject _active;
    private BaseComp _compScript;
    private int _index = 1;


    private void OnEnable()
    {
        _compScript = _active.GetComponent<BaseComp>();
        _dataUse = _data1;
        _compScript._data = _dataUse.Data;
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


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeUnits();
            _compScript._data = _dataUse.Data;
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

}
