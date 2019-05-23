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
    [SerializeField] private Transform[] _childPortrait;
    [SerializeField] private GameObject _active;
    [SerializeField] private Transform _portrait;


    private Units _dataUse;
    private BaseComp _compScript;
    private int _index = 1;

    public bool _cantSwap = false;
    private Camera _cam;
    private TargetSelector _camaTarget;
    private Transform _transCurrentTarget;


    private void OnEnable()
    {
         List<Transform> _theListPortrait = new List<Transform>();
        _portrait = this.gameObject.transform.GetChild(0);

        for(int i= 0; i < _portrait.gameObject.transform.childCount; i++)
        {
            _theListPortrait.Add(_portrait.gameObject.transform.GetChild(i));
        }

        _childPortrait = _theListPortrait.ToArray();
        _theListPortrait.Clear();

        _cam = Camera.main;
        _camaTarget = _cam.gameObject.GetComponent<TargetSelector>();
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
        
       

        if (_cantSwap == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ChangeUnits();
                SelectUnitOnTab();
                _camaTarget._target = _transCurrentTarget;
                _camaTarget.NewTarget();
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
            Player _player;
            Controller _theOne;
            _script = _units[i].GetComponent<FieldOfView>();
            _theOne = _units[i].GetComponent<Controller>();
            _player = _units[i].GetComponent<Player>();

            if(i == _index - 1)
            {
                _transCurrentTarget = _units[i].transform;
                _script._isActive = true;
                _theOne._isActive = true;
                _player._isActive = true;
            }
            else
            {
                _script._isActive = false;
                _theOne._isActive = false;
                _player._isActive = false;
            }
        }
    }

    void ChangePortrait()
    {
        for(int i = 0; i < _childPortrait.Length; i++)
        {
            
            if (i == _index - 1)
            {
                _childPortrait[i].gameObject.SetActive(true);
            }
            else
            {

            }
        }
    }

    public void IsAimaing(bool _IsOrIsnt)
    {
        
           for (int i = 0; i < _units.Length; i++)
            {
                FieldOfView _script;
                _script = _units[i].GetComponent<FieldOfView>();
                if(i == _index - 1)
                {
                    if (_IsOrIsnt == true)
                    {
                        _script._isAimaing = true;
                    _script.Refresh();
                    }
                    else
                    {
                         _script._isAimaing = false;
                    }
                }
            }  
        
    }

}
