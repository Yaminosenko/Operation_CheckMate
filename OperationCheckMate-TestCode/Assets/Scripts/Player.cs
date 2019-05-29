using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int _currentHealth;
    public bool _isActive = false;
    public Transform _portrait;
    public Weapon _weapon;
    //public bool _teamActive;
    private Image _healthImage;
    
    private int _maxHealth;
    public int _ammo; 

    //[HideInInspector]



    //[SerializeField] Units _data;

    private void Start()
    {
        //Transform _portraitChild;
        _maxHealth = 10; // A determiner par la dataBase.
        _currentHealth = _maxHealth;
        //StartCoroutine(Wait());
        //_portraitChild = _portrait.gameObject.transform.GetChild(1);
        _healthImage = _portrait.GetComponent<Image>();
        _ammo = _weapon.Ammo;
        //Debug.Log(_portrait);
    }

    public void TakeDmg(int dmg)
    {
        _currentHealth -= dmg;

        if (_currentHealth <= 0)
        {
            Debug.Log("u die");
            _currentHealth = 0;
            UpdateHealthBar();
        }
        else
        {
            UpdateHealthBar();
        }
    }

    private void Update()
    {
        //Debug.Log(_portrait);
        if (_isActive == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                TakeDmg(2);
            }
        }

    }

    public void UpdateHealthBar()
    {
        float ratio = (float)_currentHealth / (float)_maxHealth;


        Mathf.Clamp01(ratio);

        Vector3 newScale = _healthImage.transform.localScale;
        newScale.x = ratio;
        _healthImage.transform.localScale = newScale;
    }

    //private IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1);
    //}
}
