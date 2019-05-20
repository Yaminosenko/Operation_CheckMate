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

    [SerializeField] protected int _weapon;

    public int Weapon
    {
        get
        {
            return _weapon;
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
}
