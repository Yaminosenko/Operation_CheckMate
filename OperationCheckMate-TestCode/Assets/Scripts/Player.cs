using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private Image _healthOnUnit;
    public int _currentHealth;
     private int _maxHealth;
    public bool _isActive = false;
    public Transform _portrait;
    public Weapon _weapon;
    //public bool _teamActive;
    private Image _healthImage;
    
    public int _ammo; 

    //[HideInInspector]



    //[SerializeField] Units _data;

    private void Start()
    {
        
        _maxHealth = 10; // A determiner par la dataBase.
        _currentHealth = _maxHealth;
      
        _healthImage = _portrait.GetComponent<Image>();
        
        _ammo = _weapon.Ammo;
        
    }

    public void TakeDmg(int dmg) //Système de dégât 
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

    public void UpdateHealthBar() // changement de la barre de vie de l'unit
    {
        float ratio = (float)_currentHealth / (float)_maxHealth;


        Mathf.Clamp01(ratio);

        Vector3 newScale = _healthImage.transform.localScale;
        newScale.x = ratio;
        _healthImage.transform.localScale = newScale;
        _healthOnUnit.transform.localScale = newScale;
    }

    //private IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1);
    //}
}
