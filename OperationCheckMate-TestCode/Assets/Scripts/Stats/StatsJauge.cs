using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsJauge : MonoBehaviour
{
    [SerializeField] public Slider _jauge;
    [SerializeField] public Button _decrement;
    [SerializeField] public Button _increment;

    [SerializeField] private int _statsToChange = 0;

    [SerializeField] private TextMeshProUGUI _number;
    private string _numberString;
    private int _nbMax;
    public GameObject _units;
    private UnitStats _unitsScript;




    private void OnEnable()
    {

        //_units = GameObject.Find("Units");
        _unitsScript = _units.GetComponent<UnitStats>();
        _nbMax = _unitsScript._maxPtStats;
       
       
       
    }

    private void Start()
    {
        _decrement.onClick.AddListener(Decrementation);
        _increment.onClick.AddListener(Incrementation);
    }

    private void Update()
    {
        _numberString = _jauge.value.ToString();
        _number.SetText(_numberString);

        if (_statsToChange == 1)
        {
            _unitsScript._healthPoint = _jauge.value;
        }
        else if (_statsToChange == 2)
        {
            _unitsScript._stamina = _jauge.value;
        }
        else if (_statsToChange == 3)
        {
            _unitsScript._aim = _jauge.value;
        }
        else if (_statsToChange == 4)
        {
            _unitsScript._luck = _jauge.value;
        }
    }


    public void Decrementation()
    {
        if(_unitsScript._currentPtStats < _nbMax && _jauge.value != 0)
        {
            _unitsScript._currentPtStats += 1;
            _jauge.value -= 1;
            //Debug.Log("dada");
        }
    }

    public void Incrementation() 
    {
        if(_unitsScript._currentPtStats > 0 && _jauge.value != 6)
        {
            _unitsScript._currentPtStats -= 1;
            _jauge.value += 1;
            //Debug.Log("dodo");
        }
    }

}
