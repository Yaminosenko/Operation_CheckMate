using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private Image _healthOnUnit;
    public int _currentHealth;
    public int _MaxAmmo;
    private int _maxHealth;
    public bool _isActive = false;
    public bool _dead = false;
    public Transform _portrait;
    public Weapon _weapon;
    //public bool _teamActive;
    private Image _healthImage;
    public bool covered = false;
    public bool hitted = false;
    public bool _bigCover = false;
    public int _ammo;
    public int _cd = 0;
    public GameObject _triggerCover;
    public float _axisRot;
    public GameObject _muzzleShoot;
    public Animator anim;

    //[HideInInspector]



    //[SerializeField] Units _data;

    private void Start()
    {
        
        _maxHealth = 10; // A determiner par la dataBase.
        _currentHealth = _maxHealth;
      
        _healthImage = _portrait.GetComponent<Image>();
        
        _ammo = _weapon.Ammo;
        _MaxAmmo = _ammo;
        anim = gameObject.GetComponent<Animator>();
    }

    public void Shooting()
    {
        anim.SetTrigger("isShooting");
    }

    public void Reloading()
    {
        anim.SetTrigger("isReloading");
    }

 

    public void TakeDmg(int dmg) //Système de dégât 
    {
        _currentHealth -= dmg;

        if (_currentHealth <= 0)
        {
            Debug.Log("u die");
            _currentHealth = 0;
            _dead = true;
            UpdateHealthBar();
            anim.SetBool("isDead", true);
           
        }
        else
        {
            hitted = true;
            anim.SetTrigger("isHitted");
            Invoke("Safe", 2f);
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
    private void Safe()
    {
        hitted = false;
    }
   public float AnimationLength(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == name)
                time = ac.animationClips[i].length;

        return time;
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
