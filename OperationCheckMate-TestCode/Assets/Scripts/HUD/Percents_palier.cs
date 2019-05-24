using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "UnityCourse/PercentsPalier")]
public class Percents_palier : ScriptableObject
{
    [SerializeField] protected int[] _palierAim;

    public int[] PalierPercents
    {
        get
        {
            return _palierAim;
        }
    }

    [SerializeField] protected int _palierDistance;

    public int PalierDistance
    {
        get
        {
            return _palierDistance;
        }
    }
}
