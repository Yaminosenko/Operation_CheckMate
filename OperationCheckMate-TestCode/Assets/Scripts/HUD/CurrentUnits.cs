﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentUnits : MonoBehaviour
{
    [SerializeField] private Units _data1;
    [SerializeField] private Units _data2;
    [SerializeField] private Units _data3;
    [SerializeField] private Units _data4;
    [SerializeField] private Units _data5;
    [SerializeField] private Units _data6;
    [SerializeField] private Units _data7;
    [SerializeField] private Units _data8;

    private Units[] _dataTab;

    [SerializeField] private GameObject[] _team1;
    [SerializeField] private GameObject[] _team2;
    
    [SerializeField] private Transform[] _childPortrait;
    [SerializeField] private Transform[] _childPortrait2;
    [SerializeField] private Competance[] _evryComp;
    [SerializeField] private GameObject _active;
    [SerializeField] private Transform _portrait;
    [SerializeField] private Transform _portrait2;
    [SerializeField] private TeamManager _teamManagerDep;
    [SerializeField] private TextMeshProUGUI _playerTurn;

    [SerializeField] private Button _endOfTurn;
    [SerializeField] private Button _nextUnitsLeft;
    [SerializeField] private Button _nextUnitsRight;


    public Units _dataUse;
    [SerializeField] private bool _switchTeam = false;
    private BaseComp _compScript;
    public Weapon _weaponData;
    private int _index = 1;
    private float _stepHere = 0;
    private bool _Wait = false;
    public int _nbTours = 0;

    public bool _cantSwap = false;
    private Camera _cam;
    private TargetSelector _camaTarget;
    private Transform _transCurrentTarget;
    private navAgent _leNavAgent;

   [SerializeField] private List<int> _alreadyUse = new List<int>();


    private void OnEnable()
    {
        _playerTurn.SetText("Joueur 1");
        List<Units> _dataList = new List<Units>(); // convertir list en Array
         List<Transform> _theListPortrait = new List<Transform>();
         List<Transform> _theListPortrait2 = new List<Transform>();
        _portrait = this.gameObject.transform.GetChild(0);
        _portrait2 = this.gameObject.transform.GetChild(1);
        Debug.Log(_portrait2);

        _dataList.Add(_data1);
        _dataList.Add(_data2);
        _dataList.Add(_data3);
        _dataList.Add(_data4);

        _dataList.Add(_data5);
        _dataList.Add(_data6);
        _dataList.Add(_data7);
        _dataList.Add(_data8);

        _dataTab = _dataList.ToArray();
        _dataList.Clear();

        for (int i= 0; i < _portrait.gameObject.transform.childCount; i++) //recherche des portrait de chaque unité
        {
            _theListPortrait.Add(_portrait.gameObject.transform.GetChild(i));
        } //mise en place des variables dans les units des 2 teams
        for (int i = 0; i < _portrait2.gameObject.transform.childCount; i++) //recherche des portrait de chaque unité
        {
            _theListPortrait2.Add(_portrait2.gameObject.transform.GetChild(i));
        }

        _childPortrait = _theListPortrait.ToArray();
        _childPortrait2 = _theListPortrait2.ToArray();
        _theListPortrait.Clear();
        _theListPortrait2.Clear();

        for (int i = 0; i < _childPortrait.Length; i++)
        {
            Player _player;
            
            _player = _team1[i].GetComponent<Player>();
            _player._portrait = _childPortrait[i].gameObject.transform.GetChild(0);


            if (_dataTab[i].WeaponList == Units.WeaponEnum.Assault)
            {
                _player._weapon = _dataTab[i].DataList[0];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.Sniper)
            {
                _player._weapon = _dataTab[i].DataList[1];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.ShotGun)
            {
                _player._weapon = _dataTab[i].DataList[2];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.Gatling)
            {
                _player._weapon = _dataTab[i].DataList[3];
            }

        }

        for (int i = 0; i < _childPortrait.Length; i++)
        {
            Player _player;

            _player = _team2[i].GetComponent<Player>();
            _player._portrait = _childPortrait2[i].gameObject.transform.GetChild(0);


            if (_dataTab[i].WeaponList == Units.WeaponEnum.Assault)
            {
                _player._weapon = _dataTab[i].DataList[0];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.Sniper)
            {
                _player._weapon = _dataTab[i].DataList[1];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.ShotGun)
            {
                _player._weapon = _dataTab[i].DataList[2];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.Gatling)
            {
                _player._weapon = _dataTab[i].DataList[3];
            }

        }
        

        _cam = Camera.main;
        _camaTarget = _cam.gameObject.GetComponent<TargetSelector>();
        _compScript = _active.GetComponent<BaseComp>();
        _dataUse = _data1;
        _compScript._data = _dataUse.Data;
        FieldOfView _fovScript;
        _fovScript = _team1[0].GetComponent<FieldOfView>();
        _team1[0].GetComponent<navAgent>()._staminaValue = _dataUse.Stamina;
        _fovScript._isActive = true;
        _compScript._currentFov = _fovScript;

        _endOfTurn.onClick.AddListener(SwitchTeam);
        _nextUnitsLeft.onClick.AddListener(ChangeUnitsEvrywhere);
        _nextUnitsRight.onClick.AddListener(ChangeUnitsEvrywhere);
    }

    private void Update()
    {
        if(_switchTeam == false)
        {
            if (_index == 1)
            {
                _dataUse = _data1;
            } //Choix de la Database utilisé
            else if (_index == 2)
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
        }
        else
        {
            if (_index == 1)
            {
                _dataUse = _data5;
            } //Choix de la Database utilisé
            else if (_index == 2)
            {
                _dataUse = _data6;
            }
            else if (_index == 3)
            {
                _dataUse = _data7;
            }
            else if (_index == 4)
            {
                _dataUse = _data8;
            }
        }
       
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchTeam();
        }


        if (_cantSwap == false) //ne marche pas lorsqu'une action est en cours
        {
            if (_Wait == false)
            {
                if (Input.GetKeyDown(KeyCode.Tab)) //changement d'unité par touche 
                {
                    ChangeUnitsEvrywhere();
                    
                }
            }
        }
    }

    public void ChangeUnitsEvrywhere()
    {
        ChangeUnits();
        DataWeaponChange();
        SelectUnitOnTab();
        ChangePortrait();
        _camaTarget._target = _transCurrentTarget;
        _camaTarget.NewTarget();
        _teamManagerDep.soldierTurn = _index - 1;
        _compScript._data = _dataUse.Data;
        _Wait = true;
        StartCoroutine(WaitBeforeDoSomethingElse());
        _leNavAgent.ChangeUnits();
        _leNavAgent._staminaValue = _dataUse.Stamina;
        
        
    }

    public void EndOfThisUnitTurn()
    {
        _alreadyUse.Add(_index);
        
    }

    private void ChangeUnits() // change l'index de l'unité a utilisé
    {
        int[] _use = _alreadyUse.ToArray();

        _index += 1;

        if(_use.Length == 4)
        {
            SwitchTeam();
            _alreadyUse.Clear();
        }
        for (int i = 0; i < _use.Length; i++)
        {
            if (_index == _use[i])
            {
                _index += 1;
            }
        }
        if (_index > 4)
        {
            _index = 1;
        }
    }

    private void SwitchTeam()
    {
        
        _nbTours++;
        if (_switchTeam == false)
        {
            _switchTeam = true;
            _portrait.gameObject.SetActive(false);
            _portrait2.gameObject.SetActive(true);
            _transCurrentTarget = _team2[0].transform;
            _camaTarget._target = _transCurrentTarget;
            _camaTarget.NewTarget();
            _teamManagerDep.playerNum = true;
            _playerTurn.SetText("Joueur 2");
            for(int i = 0; i < _team2.Length; i++)
            {
                _team2[i].GetComponent<navAgent>()._alreadyMouv = false;
                Debug.Log("team2alreadyOn");
                
            }
        }
        else
        {
            _switchTeam = false;
            _portrait.gameObject.SetActive(true);
            _portrait2.gameObject.SetActive(false);
            _transCurrentTarget = _team1[0].transform;
            _camaTarget._target = _transCurrentTarget;
            _camaTarget.NewTarget();
            _teamManagerDep.playerNum = false;
            _playerTurn.SetText("Joueur 1");
            for (int i = 0; i < _team1.Length; i++)
            {
                _team1[i].GetComponent<navAgent>()._alreadyMouv = false;
                Debug.Log("team1alreadyOn");
            }
        }
    }


   void SelectUnitOnTab() //choisit quelle unité sera selectioné
    {
        GameObject[] _TeamTab = new GameObject[4];
        int[] _use = _alreadyUse.ToArray();

        if (_switchTeam == false)
        {
            for (int i = 0; i < _team1.Length; i++)
            {
                _TeamTab[i] = _team1[i];
              
            }
        }
        else
        {
            for (int i = 0; i < _team2.Length; i++)
            {
                _TeamTab[i] = _team2[i];

            }
        }
        
        for (int i =0; i < _TeamTab.Length; i++)
        {

            FieldOfView _script;
            Player _player;
            //Controller _theOne;
            _script = _TeamTab[i].GetComponent<FieldOfView>();
            //_theOne = _TeamTab[i].GetComponent<Controller>();
            _player = _TeamTab[i].GetComponent<Player>();
            


            if(i == _index - 1)
            {
                _transCurrentTarget = _TeamTab[i].transform;
                _script._isActive = true;
                //_theOne._isActive = true;
                _player._isActive = true;
                _compScript._currentFov = _script;
                _compScript._playerScript = _player;
                _compScript._weaponUse = _weaponData;
                _compScript._critChance = _dataUse.Luck;
                _script._baseComp = _compScript;
                _leNavAgent = _TeamTab[i].GetComponent<navAgent>();

            }
            else
            {
                _script._isActive = false;
                //_theOne._isActive = false;
                _player._isActive = false;
            }
        }
    }

  

    void ChangePortrait() //mise en place du portrait adéquat
    {
        
        Transform[] _childPortraitUse = new Transform[4];

        if (_switchTeam == false)
        {
            for (int i = 0; i < _team1.Length; i++)
            {
                _childPortraitUse[i] = _childPortrait[i];
            }
        }
        else
        {
            for (int i = 0; i < _team2.Length; i++)
            {
                _childPortraitUse[i] = _childPortrait2[i];
            }
        }

        for (int i = 0; i < _childPortraitUse.Length; i++)
        {
            
            Image _portraitImg;
            _portraitImg = _childPortraitUse[i].gameObject.GetComponent<Image>();
           
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
        GameObject[] _TeamTab = new GameObject[4];

        if (_switchTeam == false)
        {
            for (int i = 0; i < _team1.Length; i++)
            {
                _TeamTab[i] = _team1[i];

            }
        }
        else
        {
            for (int i = 0; i < _team2.Length; i++)
            {
                _TeamTab[i] = _team2[i];

            }
        }


        for (int i = 0; i < _TeamTab.Length; i++)
            {
                FieldOfView _script;
                _script = _TeamTab[i].GetComponent<FieldOfView>();
                if(i == _index - 1)
                {
                    if (_IsOrIsnt == true)
                    {
                        _script._isAimaing = true;
                        _script.Refresh();
                       // _script.AimTarget();
                        _script.FirstSelect();
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
    } //changement de l'arme en conséquence 

    IEnumerator WaitBeforeDoSomethingElse()
    {
        yield return new WaitForSeconds(3);
        _Wait = false;
    }
}
