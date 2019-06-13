using System.Collections;
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

    [SerializeField] private GameObject[] _nameTeam1;
    [SerializeField] private GameObject[] _nameTeam2;
    [SerializeField] private GameObject[] _teamName;
    
    
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
    [SerializeField] private MathCircle _mCircle;
    [SerializeField] private int _countAmmo;


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
   [SerializeField] private List<int> _deadUnits1 = new List<int>();
   [SerializeField] private List<int> _deadUnits2 = new List<int>();


    private void OnEnable()
    {
        //_playerTurn.SetText("Joueur 1");
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
                //_weaponData = _dataTab[i].DataList[0];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.Sniper)
            {
                _player._weapon = _dataTab[i].DataList[1];
                //_weaponData = _dataTab[i].DataList[1];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.ShotGun)
            {
                _player._weapon = _dataTab[i].DataList[2];
                //_weaponData = _dataTab[i].DataList[2];
            }
            else if (_dataTab[i].WeaponList == Units.WeaponEnum.Gatling)
            {
                _player._weapon = _dataTab[i].DataList[3];
                //_weaponData = _dataTab[i].DataList[3];
            }

        }

        for (int i = 0; i < _nameTeam1.Length; i++)
        {
            if (i != 0)
            {
                _nameTeam1[i].SetActive(false);
            }
            _nameTeam2[i].SetActive(false);
        }
        EnableMeshPlayer();
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
        //ChangeUnitsEvrywhere();
        
    }
    private void EnableMeshPlayer()
    {
        List<GameObject> _teamList = new List<GameObject>();
        for(int i = 0; i < _team1.Length; i++)
        {
            _teamList.Add(_team1[i]);
            _teamList.Add(_team2[i]);
        }
        GameObject[] _teamAll = _teamList.ToArray();
        
        for(int i = 0; i < _teamAll.Length; i++)
        {
            Player p = _teamAll[i].GetComponent<Player>();
            FieldOfView f = _teamAll[i].GetComponent<FieldOfView>();
            UnitsSetActive u;

            if(f._whichTeamAreYou == false)
            {
                u = p._playerMesh[0].GetComponent<UnitsSetActive>();
                p._playerMesh[1].SetActive(false);
            }
            else
            {
                u = p._playerMesh[1].GetComponent<UnitsSetActive>();
                p._playerMesh[0].SetActive(false);
            }

            if (_dataTab[i].Health >= 3)
            {
                for (int a = 0; a < u._lvl1Health.Length; a++)
                {
                    u._lvl1Health[a].SetActive(true);
                }
                u._lvl2Health.SetActive(false);
            }
            if (_dataTab[i].Health == 6)
            {
                u._lvl2Health.SetActive(true);
            }
            if (_dataTab[i].Health < 3)
            {
                for (int a = 0; a < u._lvl1Health.Length; a++)
                {
                    u._lvl1Health[a].SetActive(false);
                }
                u._lvl2Health.SetActive(false);
            }

            if (_dataTab[i].Stamina >= 3)
            {
                for (int a = 0; a < u._lvl1Stamina.Length; a++)
                {
                    u._lvl1Stamina[a].SetActive(true);
                }
                for (int a = 0; a < u._lvl2Stamina.Length; a++)
                {
                    u._lvl2Stamina[a].SetActive(false);
                }
            }
            if (_dataTab[i].Stamina == 6)
            {
                for (int a = 0; a < u._lvl2Stamina.Length; a++)
                {
                    u._lvl2Stamina[a].SetActive(true);
                }
            }
            if (_dataTab[i].Stamina < 3)
            {
                for (int a = 0; a < u._lvl1Stamina.Length; a++)
                {
                    u._lvl1Stamina[a].SetActive(false);
                }
                for (int a = 0; a < u._lvl2Stamina.Length; a++)
                {
                    u._lvl2Stamina[a].SetActive(false);
                }
            }

            if (_dataTab[i].Luck >= 3)
            {
                u._lvl1Luck.SetActive(true);
                u._lvl2Luck.SetActive(false);
            }
            if (_dataTab[i].Luck == 6)
            {
                u._lvl2Luck.SetActive(true);
            }
            if (_dataTab[i].Luck < 3)
            {
                u._lvl1Luck.SetActive(false);
                u._lvl2Luck.SetActive(false);
            }

            if (_dataTab[i].Wint == 1)
            {
                u._assault.SetActive(true);
                u._snip.SetActive(false);
                u._shotGun.SetActive(false);
                u._gatling.SetActive(false);
            }
            else if (_dataTab[i].Wint == 2)
            {
                u._assault.SetActive(false);
                u._snip.SetActive(true);
                u._shotGun.SetActive(false);
                u._gatling.SetActive(false);
            }
            else if (_dataTab[i].Wint == 3)
            {
                u._assault.SetActive(false);
                u._snip.SetActive(false);
                u._shotGun.SetActive(true);
                u._gatling.SetActive(false);
            }
            else if (_dataTab[i].Wint == 4)
            {
                u._assault.SetActive(false);
                u._snip.SetActive(false);
                u._shotGun.SetActive(false);
                u._gatling.SetActive(true);
            }

            if (_dataTab[i].Aim >= 3 && _dataTab[i].Aim != 6)
            {
                u._assaultlvl[0].SetActive(false);
                u._sniplvl[0].SetActive(false);
                u._shotGunlvl[0].SetActive(false);
                u._gatlinglvl[0].SetActive(false);

                u._assaultlvl[1].SetActive(true);
                u._sniplvl[1].SetActive(true);
                u._shotGunlvl[1].SetActive(true);
                u._gatlinglvl[1].SetActive(true);

                u._assaultlvl[2].SetActive(false);
                u._sniplvl[2].SetActive(false);
                u._shotGunlvl[2].SetActive(false);
                u._gatlinglvl[2].SetActive(false);
            }
            if (_dataTab[i].Aim == 6)
            {
                u._assaultlvl[0].SetActive(false);
                u._sniplvl[0].SetActive(false);
                u._shotGunlvl[0].SetActive(false);
                u._gatlinglvl[0].SetActive(false);

                u._assaultlvl[1].SetActive(false);
                u._sniplvl[1].SetActive(false);
                u._shotGunlvl[1].SetActive(false);
                u._gatlinglvl[1].SetActive(false);

                u._assaultlvl[2].SetActive(true);
                u._sniplvl[2].SetActive(true);
                u._shotGunlvl[2].SetActive(true);
                u._gatlinglvl[2].SetActive(true);
            }
            if (_dataTab[i].Aim < 3)
            {
                u._assaultlvl[0].SetActive(true);
                u._sniplvl[0].SetActive(true);
                u._shotGunlvl[0].SetActive(true);
                u._gatlinglvl[0].SetActive(true);

                u._assaultlvl[1].SetActive(false);
                u._sniplvl[1].SetActive(false);
                u._shotGunlvl[1].SetActive(false);
                u._gatlinglvl[1].SetActive(false);

                u._assaultlvl[2].SetActive(false);
                u._sniplvl[2].SetActive(false);
                u._shotGunlvl[2].SetActive(false);
                u._gatlinglvl[2].SetActive(false);
            }


        }

    }

    private void Update()
    {
        Debug.Log(_index);
        Debug.Log(_dataUse);
        //if (_switchTeam == false)
        //{
        //    if (_index == 1)
        //    {
        //        _dataUse = _data1;
        //    } //Choix de la Database utilisé
        //    else if (_index == 2)
        //    {
        //        _dataUse = _data2;
        //    }
        //    else if (_index == 3)
        //    {
        //        _dataUse = _data3;
        //    }
        //    else if (_index == 4)
        //    {
        //        _dataUse = _data4;
        //    }
        //}
        //else
        //{
        //    if (_index == 1)
        //    {
        //        _dataUse = _data5;
        //    } //Choix de la Database utilisé
        //    else if (_index == 2)
        //    {
        //        _dataUse = _data6;
        //    }
        //    else if (_index == 3)
        //    {
        //        _dataUse = _data7;
        //    }
        //    else if (_index == 4)
        //    {
        //        _dataUse = _data8;
        //    }
        //}
       
        
   


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

    public void DataChoice()
    {
        if (_switchTeam == false)
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
    }

    public void ChangeUnitsEvrywhere()
    {
        ChangeUnits();
        DataChoice();
        SelectUnitOnTab();
        DataWeaponChange();
        ChangePortrait();
        ChangeName();
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
        int[] _dead;
        

        if(_switchTeam == true)
        {
            _dead =  _deadUnits1.ToArray();
        }
        else
        {
            _dead = _deadUnits2.ToArray();
        }
        
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
        for(int i = 0; i<_dead.Length; i++)
        {
            if(_index == _dead[i])
            {
                _index += 1;
                Debug.Log("noUdeadSoShutUp");
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
           // _playerTurn.SetText("Joueur 2");
            _teamName[1].SetActive(true);
            _teamName[0].SetActive(false);
            _leNavAgent.ChangeUnits();
            _leNavAgent._staminaValue = _dataUse.Stamina;
           

            for (int i = 0; i < _team2.Length; i++)
            {
                _team2[i].GetComponent<navAgent>()._alreadyMouv = false;
                Player p = _team2[i].GetComponent<Player>();
                if(p._cd != 0)
                {
                    p._cd -= 1;
                }
                Debug.Log("team2alreadyOn");
            }

            for (int i = 0; i < _team1.Length; i++)
            {
                _nameTeam1[i].SetActive(false);
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
           // _playerTurn.SetText("Joueur 1");
            _teamName[1].SetActive(false);
            _teamName[0].SetActive(true);
            _leNavAgent.ChangeUnits();
            _leNavAgent._staminaValue = _dataUse.Stamina;
            
                
            for (int i = 0; i < _team1.Length; i++)
            {
                _team1[i].GetComponent<navAgent>()._alreadyMouv = false;
                Player p = _team1[i].GetComponent<Player>();
                if (p._cd != 0)
                {
                    p._cd -= 1;
                }
                Debug.Log("team1alreadyOn");
            }

            for (int i = 0; i < _team1.Length; i++)
            {
                _nameTeam2[i].SetActive(false);
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
            _script = _TeamTab[i].GetComponent<FieldOfView>();
            _player = _TeamTab[i].GetComponent<Player>();
            GrenadeAimingSystem _GAS = _TeamTab[i].GetComponentInChildren<GrenadeAimingSystem>();
            


            if(i == _index - 1)
            {
                if(_player._dead == true)
                {
                    if(_switchTeam == true)
                    {
                        _deadUnits1.Add(_index);
                    }
                    else
                    {
                        _deadUnits2.Add(_index);
                    }
                    ChangeUnitsEvrywhere();
                    //Debug.Log(_TeamTab[i]); 
                }
                _mCircle.GAS = _GAS;
                _transCurrentTarget = _TeamTab[i].transform;
                _script._isActive = true;
                _player._isActive = true;
                _compScript._currentFov = _script;
                _compScript._playerScript = _player;
                _compScript._weaponUse = _player._weapon;
                _compScript._critChance = _dataUse.Luck;
                _compScript._GAS = _GAS;
                _compScript._muzzleTransform = _player._muzzleShoot;
                _compScript.AmmoWrite();
                _script._baseComp = _compScript;
                _leNavAgent = _TeamTab[i].GetComponent<navAgent>();
                Debug.Log(_compScript._currentFov);

            }
            else
            {
                _script._isActive = false;
                //_theOne._isActive = false;
                _player._isActive = false;
            }
        }
    }

    void ChangeName()
    {

        if(_switchTeam == false)
        {
            for(int i = 0; i < _nameTeam1.Length; i++)
            {
                if(i == _index - 1)
                {
                    _nameTeam1[i].SetActive(true);
                }
                else
                {
                    _nameTeam1[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < _nameTeam1.Length; i++)
            {
                if (i == _index - 1)
                {
                    _nameTeam2[i].SetActive(true);
                }
                else
                {
                    _nameTeam2[i].SetActive(false);
                }
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
