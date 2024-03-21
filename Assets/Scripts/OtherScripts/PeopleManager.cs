using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public static event Action<Employee> OnNewEmployeeCame; //used, for example, to generate new documents for each employee
    public static event Action OnEmployeeWentAway;

    [Header("Employee parameters")]
    [SerializeField] private EmployeeObjectFactory _employeeObjectFactory;
    [SerializeField] private Transform _employeeParent;
    [SerializeField] private EmployeeObject _employeePrefab;
    [SerializeField] private float _speed;

    [Header("Employee moving points")]
    [SerializeField] private Transform _initialPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _waitPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _destroyPoint;

    private TMP_Text _resultText;

    private LevelManager _levelManager;
    private EmployeeGenerator _employeeGenerator;

    private EmployeeObject _currentEmployee;

    private bool _isCurrentlyProsessing;

    public void StartSpawningEmployees(EmployeeGenerator employeeGenerator, LevelManager levelManager)
    {
        _resultText = GameObject.Find("ResultText").GetComponent<TMP_Text>();

        _employeeGenerator = employeeGenerator;
        _levelManager = levelManager;

        _employeeObjectFactory = new EmployeeObjectFactory(_employeeParent, _employeePrefab);

        SpawnNextEmployee();
        _currentEmployee.Employee.DebugShowEmployee();
        Debug.Log("StartSpawningEmployees");
    }
   private void SpawnNextEmployee()
    {
        _isCurrentlyProsessing = false;
        Employee nextEmployee = _employeeGenerator.GetNextEmployee();
        _currentEmployee = _employeeObjectFactory.GetEmployee(nextEmployee, _initialPoint, _startPoint, _waitPoint, _endPoint, _destroyPoint);
        _currentEmployee.OnPlaceArrived += delegate { OnNewEmployeeCame?.Invoke(_currentEmployee.Employee); _isCurrentlyProsessing = true; };  // Invoking OnNewEmployeeCame, DocumentGanerator is the subscriber 
        _currentEmployee.StartMoving(_speed);


        //_currentEmployee.Employee.DebugShowEmployee();
        //spawn
    }
    private void RemoveCurrentEmployee()
    {
        _currentEmployee.EndMoving();
        _currentEmployee = null;
    }
    public void DecideFateOfTheWorker(bool shouldBeFired)
    {
        if (!_isCurrentlyProsessing || GameManager.Instance.State != GameState.Playing)
            return;

        if (_currentEmployee.Employee.IsLate == shouldBeFired)
        {
            _resultText.text = "Correct";
        }
        else
        {
            _resultText.text = "Incorrect" + " -1HP";
            _levelManager.PlayerHealth--;
        }

        RemoveCurrentEmployee();

        try
        {
            SpawnNextEmployee();
        }
        catch
        {
            _isCurrentlyProsessing = false;
            _resultText.text = "You Won! ";
            _levelManager.ShowWinScreen();
        }
    }
}
