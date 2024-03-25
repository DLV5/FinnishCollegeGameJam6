using System.Collections.Generic;
using UnityEngine;

public class EmployeeVisualisationFactory: ScriptableObject
{
    private EmployeeVisualisation _prefab;
    private Transform _parent;

    public EmployeeVisualisationFactory(Transform parent, EmployeeVisualisation prefab)
    {
        _prefab = prefab;
        _parent = parent;
    }
    public EmployeeVisualisation GetEmployee(Employee employee, Queue<Vector3> pathToMove)
    {
        EmployeeVisualisation instance = Instantiate(_prefab, _parent);
        Debug.Log("Made instance" + instance);
        instance.SetUp(employee, pathToMove);
        return instance;
    }
}
