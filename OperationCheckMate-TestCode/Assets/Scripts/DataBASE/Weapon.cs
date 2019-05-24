using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "UnityCourse/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] protected float _scope;

    public float Scope
    {
        get
        {
            return _scope;
        }
    }

    [SerializeField] protected int _dmg;

    public int Damage
    {
        get
        {
            return _dmg;
        }
    }

    [SerializeField] protected int _ammo;

    public int Ammo
    {
        get
        {
            return _ammo;
        }
    }
}
