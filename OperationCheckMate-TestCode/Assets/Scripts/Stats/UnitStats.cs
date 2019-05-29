using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitStats : MonoBehaviour
{
    public int _maxPtStats = 16;
    public int _currentPtStats = 16;


    public float _healthPoint = 0;
    public float _stamina = 0;
    public float _aim = 0;
    public float _luck = 0;
    public float _weapon = 0;

    [SerializeField] private TextMeshProUGUI _number;
    private string _numberString;

    private void Update()
    {
        _numberString = _currentPtStats.ToString();
        _number.SetText(_numberString);

        //Debug.Log(_currentPtStats);

        Debug.Log(StaticStats1.Health);
    }
}
