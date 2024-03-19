using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _ageText;
    [SerializeField] private TMP_Text _arrivalTime;
    public void Initialize(Employee employee)
    {
        _nameText.text =  $" Name: {employee.Name} {employee.Surname}";
        _ageText.text = $" Age: {employee.Age}";
        _arrivalTime.text = $" Arrival Time: { employee.ArrivedAtTime.ToShortTimeString()}";
    }
}
