using System.Collections.Generic;
using UnityEngine;

public class ScheduleGenerator : MonoBehaviour
{
    [SerializeField] private Transform _infoParent;
    [SerializeField] private InfoObject _infoPrefab;
    public void InitializeSchedule(Queue<Employee> employees)
    {
        foreach (Employee employee in employees)
        {
            InfoObject instance = Instantiate(_infoPrefab, _infoParent);
            instance.Initialize(employee);
        }
    }
}