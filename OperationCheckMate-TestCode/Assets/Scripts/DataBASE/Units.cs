using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "UnityCourse/Units")]
public class Units : ScriptableObject
{
    [SerializeField] protected float _health;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    [SerializeField] protected float _stamina;

    public float Stamina
    {
        get
        {
            return _stamina;
        }
        set
        {
            _stamina = value;
        }
    }

    [SerializeField] protected float _aim;

    public float Aim
    {
        get
        {
            return _aim;
        }
        set
        {
            _aim = value;
        }
    }

    [SerializeField] protected float _luck;

    public float Luck
    {
        get
        {
            return _luck;
        }
        set
        {
            _luck = value;
        }
    }

 
    [SerializeField] protected Competance _data;

    public Competance Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }

    public enum WeaponEnum
    {
        Assault, Sniper, ShotGun, Gatling 
    }

    [SerializeField] protected WeaponEnum _weaponList;
    public WeaponEnum WeaponList
        {
            get
            {
                return _weaponList;
            }
        set
        {
            _weaponList = value;
        }
    }

    [SerializeField] protected Weapon[] _dataList;
    public Weapon[] DataList
    {
        get
        {
            return _dataList;
        }
    }
}
