using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int _currentHealth;
    public bool _isActive = false;
    private int _maxHealth;

    [SerializeField] Units _data;

    private void OnEnable()
    {
        _maxHealth = 10; // A determiner par la dataBase.
        _currentHealth = _maxHealth;
    }

    public void OnTakeDmg(int dmg)
    {
        _currentHealth -= dmg;
    }

    private void Update()
    {
        if (_isActive == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                OnTakeDmg(2);

                if (_currentHealth <= 0)
                {
                    Debug.Log("u die");
                }
            }
        }

    }
}
