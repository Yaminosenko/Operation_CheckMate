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
    public Weapon _weaponData;
    private int _index = 1;

    public bool _cantSwap = false;
    private Camera _cam;
    private TargetSelector _camaTarget;
    private Transform _transCurrentTarget;


    private void OnEnable()
    {
         List<Transform> _theListPortrait = new List<Transform>();
        _portrait = this.gameObject.transform.GetChild(0);

        for(int i= 0; i < _portrait.gameObject.transform.childCount; i++) //recherche des portrait de chaque unité
        {
            _theListPortrait.Add(_portrait.gameObject.transform.GetChild(i));
        }

        _childPortrait = _theListPortrait.ToArray();
        _theListPortrait.Clear();

        for (int i = 0; i < _childPortrait.Length; i++)
        {
            Player _player;
            _player = _units[i].GetComponent<Player>();
            _player._portrait = _childPortrait[i].gameObject.transform.GetChild(0);
        }

        _cam = Camera.main;
        _camaTarget = _cam.gameObject.GetComponent<TargetSelector>();
        _compScript = _active.GetComponent<BaseComp>();
        _dataUse = _data1;
        _compScript._data = _dataUse.Data;
        FieldOfView _fovScript;
        _fovScript = _units[0].GetComponent<FieldOfView>();
        _fovScript._isActive = true;
        _compScript._currentFov = _fovScript;


       

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
        
       

        if (_cantSwap == false) //ne marche pas lorsqu'une action est en cours
        {
            if (Input.GetKeyDown(KeyCode.Tab)) //changement d'unité par touche 
            {
                ChangeUnits();
                SelectUnitOnTab();
                ChangePortrait();
                DataWeaponChange();
                _camaTarget._target = _transCurrentTarget;
                _camaTarget.NewTarget();
                _compScript._data = _dataUse.Data;
            }
        }
    }

    private void ChangeUnits() // change l'index de l'unité a utilisé
    {
        _index += 1;

        if (_index > 4)
        {
            _index = 1;
        }
    }

   void SelectUnitOnTab() //choisit quelle unité sera selectioné
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
                _compScript._currentFov = _script;
            }
            else
            {
                _script._isActive = false;
                _theOne._isActive = false;
                _player._isActive = false;
            }
        }
    }

    void ChangePortrait() //mise en place du portrait adéquat
    {
        for(int i = 0; i < _childPortrait.Length; i++)
        {
            
            Image _portraitImg;
            _portraitImg = _childPortrait[i].gameObject.GetComponent<Image>();
           
            var _temp = _portraitImg.color;
            
            if (i == _index - 1)
            {
                _temp.a = 1f;
                _portraitImg.color = _temp;
            }
            else
            {
                _temp.a = 0.5f;
                _portraitImg.color = _temp;
            }
        }
    }

    public void IsAimaing(bool _IsOrIsnt) // vise t-il ?
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

    private void DataWeaponChange()
    {
        if (_dataUse.WeaponList == Units.WeaponEnum.Assault)
        {
            _weaponData = _dataUse.DataList[0];
        }
        if (_dataUse.WeaponList == Units.WeaponEnum.Sniper)
        {
            _weaponData = _dataUse.DataList[1];
        }
        if (_dataUse.WeaponList == Units.WeaponEnum.ShotGun)
        {
            _weaponData = _dataUse.DataList[2];
        }
        if (_dataUse.WeaponList == Units.WeaponEnum.Gatling)
        {
            _weaponData = _dataUse.DataList[3];
        }
    }

}
