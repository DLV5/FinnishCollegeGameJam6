using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _ageText;
    [SerializeField] private TMP_Text _Text;
    public void Initialize(Employee employee)
    {
        _nameText.text = employee.Name + " " + employee.Surname;
    }
}
