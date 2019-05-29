using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStats2
{
    private static float health, stamina, aim, luck, weapon;

    public static float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public static float Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
        }
    }

    public static float Aim
    {
        get
        {
            return aim;
        }
        set
        {
            aim = value;
        }
    }

    public static float Luck
    {
        get
        {
            return luck;
        }
        set
        {
            luck = value;
        }
    }

    public static float Weapon
    {
        get
        {
            return weapon;
        }
        set
        {
            weapon = value;
        }
    }
}
