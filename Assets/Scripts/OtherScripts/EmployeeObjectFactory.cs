 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeObjectFactory: ScriptableObject
{
    private EmployeeObject _prefab;
    private Transform _parent;

    public EmployeeObjectFactory(Transform parent, EmployeeObject prefab)
    {
        _prefab = prefab;
        _parent = parent;
    }
    public EmployeeObject GetEmployee(Employee employee, Transform initialPoint, Transform startPoint, Transform waitPoint, Transform endPoint, Transform destroyPoint)
    {
        EmployeeObject instance = Instantiate(_prefab, _parent);
        Debug.Log("Made instance" + instance);
        instance.SetUp(employee, initialPoint, startPoint, waitPoint, endPoint, destroyPoint);
        return instance;
    }
}
