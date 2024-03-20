using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public event Action<Employee> OnNewEmployeeCame;
    public event Action OnEmployeeWentAway;

    [SerializeField] private EmployeeObjectFactory _employeeObjectFactory;

    [SerializeField] private Transform _initialPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _waitPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _destroyPoint;
    private EmployeeObject _currentEmployee;
   public void SpawnEmployee(Employee employee)
    {
        _currentEmployee = _employeeObjectFactory.GetEmployee(employee, _initialPoint, _startPoint, _waitPoint, _endPoint, _destroyPoint);
        _currentEmployee.OnPlaceArrived += delegate { OnNewEmployeeCame?.Invoke(_currentEmployee.Employee); };  // Invoking OnNewEmployeeCame, DocumentGanerator is the subscriber 
        _currentEmployee.StartMoving();
        //spawn
    }
    public void RemoveEmployee()
    {
        _currentEmployee.EndMoving();
    }
}
