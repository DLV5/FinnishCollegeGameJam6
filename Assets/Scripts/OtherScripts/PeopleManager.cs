using System;
using System.Collections;
using System.Collections.Generic;
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

    private HealthManager HealthManager;
    private EmployeeGenerator _employeeGenerator;
    private EmployeeObject _currentEmployee;

    private bool _isCurrentlyProsessing;

    public void StartSpawningEmployees(EmployeeGenerator employeeGenerator)
    {
        _employeeGenerator = employeeGenerator;

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
        if (!_isCurrentlyProsessing)
            return;

        if (_currentEmployee.Employee.IsLate == shouldBeFired)
        {
            Debug.LogWarning("Correct");
        }
        else
        {
            Debug.LogWarning("Incorrect");
            Debug.LogWarning("-1HP");
            //_resultText.text = "Incorrect" + " -1HP";
            //PlayerHealth--;
        }

        RemoveCurrentEmployee();

        try
        {
            SpawnNextEmployee();
        }
        catch
        {
            Debug.LogWarning("Show win screen");
            //_winScreen.SetActive(true);
            //UnlockCursorAndFreezeCamera();
        }
    }
}
