 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEmployeeObjectFactory", menuName = "Employee")]
public class EmployeeObjectFactory : ScriptableObject
{
    [SerializeField] private EmployeeObject _prefab;
    [SerializeField] private Transform _parent;

    public EmployeeObject GetEmployee(Employee employee, Transform initialPoint, Transform startPoint, Transform waitPoint, Transform endPoint, Transform destroyPoint)
    {
        EmployeeObject instance = Instantiate(_prefab, _parent);
        instance.SetUp(employee, initialPoint, startPoint, waitPoint, endPoint, destroyPoint);
        return instance;
    }
}
