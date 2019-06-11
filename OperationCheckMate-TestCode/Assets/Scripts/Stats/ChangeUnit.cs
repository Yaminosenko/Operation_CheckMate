using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUnit : MonoBehaviour
{
    [SerializeField] private GameObject _unitNb1;
    [SerializeField] private GameObject _unitNb2;
    [SerializeField] private GameObject _unitNb3;
    [SerializeField] private GameObject _unitNb4;

    [SerializeField] private GameObject _name1;
    [SerializeField] private GameObject _name2;
    [SerializeField] private GameObject _name3;
    [SerializeField] private GameObject _name4;

    [SerializeField] public Button _right;
    [SerializeField] public Button _left;

    private int _index = 1;

    private void OnEnable()
    {
        _right.onClick.AddListener(UnitRight);
        _left.onClick.AddListener(UnitLeft);
    }

    private void Update()
    {
        if (_index == 1)
        {
            _unitNb1.SetActive(true);
            _unitNb2.SetActive(false);
            _unitNb3.SetActive(false);
            _unitNb4.SetActive(false);

            _name1.SetActive(true);
            _name2.SetActive(false);
            _name3.SetActive(false);
            _name4.SetActive(false);
        }

        else if (_index == 2)
        {
            _unitNb1.SetActive(false);
            _unitNb2.SetActive(true);
            _unitNb3.SetActive(false);
            _unitNb4.SetActive(false);

            _name1.SetActive(false);
            _name2.SetActive(true);
            _name3.SetActive(false);
            _name4.SetActive(false);
        }

        else if (_index == 3)
        {
            _unitNb1.SetActive(false);
            _unitNb2.SetActive(false);
            _unitNb3.SetActive(true);
            _unitNb4.SetActive(false);

            _name1.SetActive(false);
            _name2.SetActive(false);
            _name3.SetActive(true);
            _name4.SetActive(false);
        }

        else if (_index == 4)
        {
            _unitNb1.SetActive(false);
            _unitNb2.SetActive(false);
            _unitNb3.SetActive(false);
            _unitNb4.SetActive(true);

            _name1.SetActive(false);
            _name2.SetActive(false);
            _name3.SetActive(false);
            _name4.SetActive(true);
        }
    }

    private void UnitLeft()
    {
       

        _index -= 1;

        if (_index <= 0)
        {
            _index = 4;
        }
    }

    private void UnitRight()
    {
        

        _index += 1;

        if (_index > 4)
        {
            _index = 1;
        }
    }

}
