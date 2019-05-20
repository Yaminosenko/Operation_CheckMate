using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "UnityCourse/Competance")]
public class Competance : ScriptableObject
{
    [SerializeField] protected Sprite _icon;

    public Sprite Icone
    {
        get
        {
            return _icon;
        }
    }

    [SerializeField] protected int _index;

    public int Index
    {
        get
        {
            return _index;
        }
    }



}
