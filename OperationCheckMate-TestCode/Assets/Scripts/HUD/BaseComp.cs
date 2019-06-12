using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseComp : MonoBehaviour
{
     [SerializeField] private Button _active;
     [SerializeField] private Button _shoot;
     [SerializeField] private Button _overwatch;
     [SerializeField] private Button _defense;
     [SerializeField] private Button _grenade;
     [SerializeField] private Button _reload;
     [SerializeField] private GameObject _TabInformation;
     [SerializeField] private GameObject _percents;
     [SerializeField] private Percents_palier _equilibreDataPercents;
     [SerializeField] private GameObject _camToInst;
     [SerializeField] private CameraSwitch2 _camSwitch;
     [SerializeField] private TextMeshProUGUI _targetInView;
     [SerializeField] private TextMeshProUGUI _txtPercents;

     [SerializeField] private GameObject _fxMuzzleFalsh;
     [SerializeField] private GameObject _fxProjectile;
     public GameObject _muzzleTransform;
     

     public Competance _data;
     public GrenadeAimingSystem _GAS;

     public GameObject _canvas;
     private Camera _cam;
     public CurrentUnits _scriptCurrent;
     public float _critChance;
     
     public CameraMouv _camMouv;
     public Weapon _weaponUse;
     public Sprite _sprite;
     private int _index;
     private int _indexOfComp;
     private int _dmg;
     private float _bulletSpeed;
     

    private string _shootTxt = "Oui le tire";
    private string _ovTxt = "Oui la Vigilence";
    private string _defenseTxt = "Oui le defense";
    private string _grenadeTxt = "Oui la grenade";
    private string _reloadTxt = "Oui le Reload";
    private string _ActiveTxt = "Oui l'active";

    private TextMeshProUGUI _tabInfoText;
    private TargetSelector _camScript;
    public FieldOfView _currentFov;
    public Player _playerScript;

    private float _scope;
    private float _distanceTargetPercent;
    private int _protectLvl;
    private bool _camIsInstanciat = false;
    [SerializeField] private float _percentsFinal;
    [SerializeField] private float _distanceTarg;


    private void OnEnable()
    {
        _cam = Camera.main;
        //_canvas = GameObject.Find("Canvas");
        _scriptCurrent = _canvas.GetComponent<CurrentUnits>();
        _camScript = _cam.GetComponent<TargetSelector>();
       // _camMouv = _camToInst.GetComponent<CameraMouv>();
        //_camSwitch = _camswap.GetComponent<CameraSwitch>(); 
        _active.onClick.AddListener(ActiveButton);
        _shoot.onClick.AddListener(ShootButton);
        _overwatch.onClick.AddListener(OverwatchButton);
        _defense.onClick.AddListener(DefenseButton);
        _grenade.onClick.AddListener(GrenadeButton);
        _reload.onClick.AddListener(ReloadButton);

        _bulletSpeed = _fxProjectile.GetComponent<Projectile_move>().speed;

        _tabInfoText = _TabInformation.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() 
    {
        _sprite = _data.Icone;
        _index = _data.Index;

        _active.image.sprite = _sprite;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootButton();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OverwatchButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DefenseButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GrenadeButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ReloadButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ActiveButton();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._cantSwap = false;
            _currentFov.Refresh();
            _camIsInstanciat = false;
            _camSwitch.ResetCamera();
            _currentFov.focused = null;
            _camMouv.DestroyObject();
            _GAS._isActive = false;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (_indexOfComp == 1)
            {
                Shoot(_currentFov.gameObject.transform);
            }
            if(_indexOfComp == 5)
            {
                ReloadAmmo();
            }
            if(_indexOfComp == 2)
            {
                _currentFov._isOverwatched = true;
                _scriptCurrent.EndOfThisUnitTurn();
                _scriptCurrent.ChangeUnitsEvrywhere();
                _playerScript._cd = 2;
            }
            _indexOfComp = 0;
            _TabInformation.SetActive(false);
            _percents.SetActive(false);
            _scriptCurrent._cantSwap = false;

        }

        if(_currentFov._swap == true)
        {
            PercentsCalcul(_currentFov, false);
        }

        string _txt;

        _txt = _currentFov._listNb.ToString();
        _targetInView.SetText(_txt);
        
    }

    private void ShootButton()
    {
        if(_currentFov._listNb != 0)
        {
             if(_playerScript._ammo > 0)
             {
                _indexOfComp = 1;
                _tabInfoText.SetText(_shootTxt);
                AnyButtonDown();
             }
        }
    }// tout les boutons

    private void OverwatchButton()
    {
        if(_playerScript._cd == 0)
        {
            _indexOfComp = 2;
            _tabInfoText.SetText(_ovTxt);
            AnyButtonDown();
        }
    }

    private void DefenseButton()
    {
        _indexOfComp = 3;
        _tabInfoText.SetText(_defenseTxt);
        AnyButtonDown();
    }

    private void GrenadeButton()
    {
        //seulement lorsque il a encore une grenade
        _indexOfComp = 4;
        _tabInfoText.SetText(_grenadeTxt);
        AnyButtonDown();
        _GAS._isActive = true;
    }

    private void ReloadButton()
    {
        //seulement quand il lui manque au moins une munition
        _indexOfComp = 5;
        _tabInfoText.SetText(_reloadTxt);
        AnyButtonDown();
    }

    private void ActiveButton()
    {
        _indexOfComp = 6;
        _tabInfoText.SetText(_ActiveTxt);
        AnyButtonDown();
    }

    private void AnyButtonDown() // Affichage du txt du bouton
    {
        
        if(_indexOfComp == 1)
        {
            _percents.SetActive(true);
            _scriptCurrent.IsAimaing(true);
            _GAS._isActive = false;

            if(_camIsInstanciat == false)
            {
                InstanciateCamera();
                _camIsInstanciat = true;
                _camMouv._isActive = true;
            }
        }   
        else if(_indexOfComp == 4)
        {
            _GAS._isActive = true;
        }
        else
        {
            _percents.SetActive(false);
            _scriptCurrent.IsAimaing(false);
            _camIsInstanciat = false;
            _GAS._isActive = false;
            
            if (_camMouv != null)
            {
                _camSwitch.ResetCamera();
                _camMouv.DestroyObject();
                _camMouv = null;
            }

        }

          
          _TabInformation.SetActive(true);
          _scriptCurrent._cantSwap = true;
    }

    private void ReloadAmmo() // fin de tours apres Reload
    {
        _playerScript._ammo = _playerScript._MaxAmmo; // + lancé animation 
        _playerScript.Reloading();
        Invoke("ReloadWait", _playerScript.AnimationLength("reload"));

        //Debug.Log(_playerScript._ammo);
    }

    public void ReloadWait()
    {
        _scriptCurrent.EndOfThisUnitTurn();
        _scriptCurrent.ChangeUnitsEvrywhere();
    }

    public void Bullet()
    {
        Transform _target;
        
        _target = _currentFov._actualTarget;
        GameObject go = Instantiate(_fxProjectile, _muzzleTransform.transform.position, Quaternion.identity);
        go.transform.LookAt(_target.position);
        Destroy(go, 3f);

    }

    public void ChangeAfterShoot()
    {
        _scriptCurrent.EndOfThisUnitTurn(); // fin du tour de cette unité 
        _scriptCurrent.IsAimaing(false);
        _scriptCurrent._cantSwap = false;
        _currentFov.Refresh();
        _camIsInstanciat = false;
        _camSwitch.ResetCamera();
        _scriptCurrent.ChangeUnitsEvrywhere(); // change l'unité selectionnée
        _camMouv.DestroyObject();
    }
    public void Hitting()
    {
        Transform _target;
        Player _playerTarget;
        _target = _currentFov._actualTarget;
        if (_target != null)
        {
                _playerTarget = _target.GetComponent<Player>();
                _playerTarget.TakeDmg(_dmg);
        }
    }
    public void Hitting2()
    {
        Transform _target;
        Player _playerTarget;
        _target = _currentFov._actualTarget;
        if (_target != null)
        {
            _playerTarget = _target.GetComponent<Player>();
            _playerTarget.TakeDmg(_dmg + 3);
        }
    }
    public void Shoot(Transform _shooter) // tir sur une cible donnée + fin de tour 
    {
        
        Transform _target;
        Player _playerTarget;
        _target = _currentFov._actualTarget;
        if (_target != null)
        {
           
            _playerTarget = _target.GetComponent<Player>();
            _dmg = _weaponUse.Damage;
            _playerScript._ammo -= 1;
            Invoke("Bullet", _playerScript.AnimationLength("shoot") / 2);
            Invoke("ChangeAfterShoot", _playerScript.AnimationLength("shoot"));
            _playerScript.Shooting();

            if (RandomShoot() == true) // tir réussi animation tir dans unité ennemis 
            {
                if (CriticalChance() == true)
                {
                    float hitTime = Vector3.Distance(_playerScript.gameObject.transform.position, _target.transform.position) / _bulletSpeed;//a changer
                    Invoke("Hitting2", hitTime);
                    
                }
                else
                {
                    float hitTime = Vector3.Distance(_playerScript.gameObject.transform.position, _target.transform.position) / _bulletSpeed;//a changer
                    Invoke("Hitting", hitTime);
                }
            }
            else
            {

                // v = d / t    ->    t = d / v
                float hitTime =  Vector3.Distance(_playerScript.gameObject.transform.position, _target.transform.position) / _bulletSpeed;//a changer
                Invoke("Hitting", hitTime);
            }


          
        }
    }

 


    public void PercentsCalcul(FieldOfView _fov, bool _ov) //calcule du pourcentage de chance de toucher la cible 
    {
        float _coverValue = 0;
         _distanceTarg = _currentFov._distanceTarget;

        
        if (_fov._actuaTargetCover == 1)
        {
            _coverValue = 20f;
        }
        else if(_fov._actuaTargetCover == 2)
        {
            _coverValue = 40f;
        }
        else if(_fov._actuaTargetCover == 3)
        {
            _coverValue = -20;
        }
        else if(_fov._actuaTargetCover == 0)
        {
            _coverValue = 50;
        }

        if (_fov._isFlank == true)
        {
            _coverValue = -20;
        }



        _scope = _scriptCurrent._weaponData.Scope;

        if (_scriptCurrent._weaponData == _scriptCurrent._dataUse.DataList[1])
        {
            if(_distanceTarg <= 10)
            {
                _percentsFinal = 24;
            }
        }
        else if (_scriptCurrent._weaponData == _scriptCurrent._dataUse.DataList[2])
        {
            if(_distanceTarg >= 20)
            {
                _percentsFinal = 24;
            }
            else if (_distanceTarg <= 10)
            {
                _percentsFinal = _scope - _distanceTarg - _coverValue + 20;
                if (_ov == true)
                {
                    _percentsFinal -= 20;
                }
            }
            else
            {
                _percentsFinal = _scope - _distanceTarg - _coverValue;
                if (_ov == true)
                {
                    _percentsFinal -= 20;
                }
            }
        }
        else
        {
            _percentsFinal = _scope - _distanceTarg - _coverValue;
           if(_ov == true)
           {
               _percentsFinal -= 20;
           }
        }


        if (_percentsFinal < 10f)
        {
            _percentsFinal = 0;
        }
        else if (_percentsFinal > 10 && _percentsFinal < 25)
        {
            _percentsFinal = 25f;
        }
        else if (_percentsFinal > 25 && _percentsFinal < 50)
        {
            _percentsFinal = 50f;
        }
        else if (_percentsFinal > 50 && _percentsFinal < 75)
        {
            _percentsFinal = 75f;
        }
        else if (_percentsFinal > 75)
        {
            _percentsFinal = 100;
        }
        string _txt = _percentsFinal.ToString();
        _txtPercents.SetText(_txt);
        _fov._swap = false;
    }

    public bool RandomShoot() // Choix du randome grace au pourcentage converti en palier pour une meillieurs expérience de jeu moins frustrante
    {
        float _value = 0f; 
        float _success; 
        
        if (_percentsFinal < 10f)
        {
            return false; 
        }
        else if (_percentsFinal > 10 && _percentsFinal < 25)
        {
            _value = 25f;
        }
        else if (_percentsFinal > 25 && _percentsFinal < 50)
        {
            _value = 50f;
        }
        else if (_percentsFinal > 50 && _percentsFinal < 75)
        {
            _value = 75f;
        }
        else if (_percentsFinal > 75)
        {
            return true;
        }

        _success = Random.Range(0, 100);
        //Debug.Log(_success);

        if (_success < _value)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CriticalChance()
    {
        float _critChanceF = 10;

        _critChanceF += 2.5f * _critChance;

        float _success = Random.Range(0, 100);
        if(_success <= _critChanceF)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Overwatch(Player _p, Weapon _w, FieldOfView _f)
    {
        Transform _target;
        Player _playerTarget;

        _target = _f._actualTarget;
        if (_target != null)
        {
            PercentsCalcul(_f, true);
            _playerTarget = _target.GetComponent<Player>();
            _dmg = _w.Damage;
            _p._ammo -= 1;

            if (RandomShoot() == true) // tir réussi animation tir dans unité ennemis 
            {
                if (CriticalChance() == true)
                {
                    _playerTarget.TakeDmg(_dmg + 3);
                }
                else
                {
                    _playerTarget.TakeDmg(_dmg);
                    //Debug.Log(_dmg);
                }
            }
            else
            {
                // animation echec tir 
            }
        }
    }

 

    private void InstanciateCamera()
    {
        
        GameObject _currentCam = (GameObject)Instantiate(_camToInst, _cam.transform.position, _cam.transform.rotation) as GameObject;
        //Debug.Log(_currentCam);
        _camMouv = _currentCam.GetComponent<CameraMouv>();
        _camSwitch.cameraOne = _currentCam;
        _camSwitch.cameraChangeCounter();
        _camMouv.views[0] = _currentFov._CamPos;
        _camMouv.LetsGo();
        _currentFov._camMouvScript = _camMouv;  
        
        if (_currentFov._actualTarget != null)
        {
            _currentFov._actualTarget = _camMouv.targetObj;
        }

    }

}
