using TMPro;
using UnityEngine;

public class DocumentsGenerator : MonoBehaviour
{
    [SerializeField] private PeopleManager _peopleManager;

    [SerializeField] private TMP_Text _idName;
    [SerializeField] private TMP_Text _idAge;
    [SerializeField] private TMP_Text _idYearOfBirth;

    private void OnEnable()
    {
        _peopleManager.OnNewEmployeeCame += SetUpDocuments;
    }

    private void OnDisable()
    {
        _peopleManager.OnNewEmployeeCame -= SetUpDocuments;
    }

    private void SetUpDocuments(Employee currentEmployee)
    {
        Debug.Log("Documents were setted up");
        SetUpID(currentEmployee);
    }

    private void SetUpID(Employee currentEmployee)
    {
        _idName.text = "Name: " + currentEmployee.Name;
        _idAge.text = "Age: " +currentEmployee.Age.ToString();
        _idYearOfBirth.text = "Birth Date: " + currentEmployee.DateOfBirth.ToShortDateString();
    }

}
