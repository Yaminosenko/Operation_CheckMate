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

    [SerializeField] private GameObject _assaultTxt;
    [SerializeField] private GameObject _snipeTxt;
    [SerializeField] private GameObject _sgTxt;
    [SerializeField] private GameObject _gatlingTxt;

    private int _index = 1;
    public  GameObject _units;
    private UnitStats _unitsScript;



    private void Start()
    {
        _weaponRight.onClick.AddListener(WeaponRight);
        _weaponLeft.onClick.AddListener(WeaponLeft);


        _unitsScript = _units.GetComponent<UnitStats>();
    }




    public void WeaponLeft() //bouton gauche = arme de gauche 
    {
        _index -= 1;
        if (_index <= 0)
        {
            _index = 4;
        }
        _unitsScript._weapon = _index;
        ChangeSprit();
        Debug.Log(_index);
    }

    public void WeaponRight() // bouton droite = arme droite
    {
        _index += 1;
        //Debug.Log(_index);
        ChangeSprit();
        _unitsScript._weapon = _index;
        if (_index >= 4)
        {
            _index = 0;
        }
    }

    private void ChangeSprit() //changement du sprite de l'arme sélectionné 
    {
        if(_index == 1)
        {
            _weaponImage.sprite = _assault;
            _assaultTxt.SetActive(true);
            _snipeTxt.SetActive(false);
            _sgTxt.SetActive(false);
            _gatlingTxt.SetActive(false);
        }
        else if (_index == 2)
        {
            _weaponImage.sprite = _sniper;
            _assaultTxt.SetActive(false);
            _snipeTxt.SetActive(true);
            _sgTxt.SetActive(false);
            _gatlingTxt.SetActive(false);
        }
        else if (_index == 3)
        {
            _weaponImage.sprite = _shotgun;
            _assaultTxt.SetActive(false);
            _snipeTxt.SetActive(false);
            _sgTxt.SetActive(true);
            _gatlingTxt.SetActive(false);
        }
        else if (_index == 4)
        {
            _weaponImage.sprite = _gatling;
            _assaultTxt.SetActive(false);
            _snipeTxt.SetActive(false);
            _sgTxt.SetActive(false);
            _gatlingTxt.SetActive(true);
        }
    }
}
