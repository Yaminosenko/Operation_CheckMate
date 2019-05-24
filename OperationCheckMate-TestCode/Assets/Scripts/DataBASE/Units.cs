using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "UnityCourse/Units")]
public class Units : ScriptableObject
{
    [SerializeField] protected int _health;

    public int Health
    {
        get
        {
            return _health;
        }
    }

    [SerializeField] protected int _stamina;

    public int Stamina
    {
        get
        {
            return _stamina;
        }
    }

    [SerializeField] protected int _aim;

    public int Aim
    {
        get
        {
            return _aim;
        }
    }

    [SerializeField] protected int _luck;

    public int Luck
    {
        get
        {
            return _luck;
        }
    }

 
    [SerializeField] protected Competance _data;

    public Competance Data
    {
        get
        {
            return _data;
        }
    }

    public enum WeaponEnum
    {
        Assault, Sniper, ShotGun, Gatling, 
    }

    [SerializeField] protected WeaponEnum _weaponList;
    public WeaponEnum WeaponList
        {
            get
            {
                return _weaponList;
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
